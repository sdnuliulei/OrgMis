//页面加载
$(document).ready(function () {
    operation.init();
});
var operation = {
    url: "",
    init: function () {
        this.loadRightTypes();
        $("#add_").click(this.createRight);
        $("#edit_").click(this.editRight);
        $("#delete_").click(this.delRights);
        $("#btn_Confirm_").click(this.saveRight);
    }, //初始化
    createRight: function () {
        $('#dlg').dialog('open').dialog('setTitle', '新建权限');
        $('.fm').form('clear');
        operation.url = "Create";
        $.post("/Admin/RightTypes/Index", null, function (data) {
            $("#RTID").html("");
            $.each(data, function (k, v) {
                $("#RTID").append("<option value=\"" + v.RTID + "\">" + v.RTypeName + "</option>");
            });
        }, "json");
    },
    editRight: function () {
        $.post("/Admin/RightTypes/Index", null, function (data) {
            $("#RTID").html("");
            $.each(data, function (k, v) {
                $("#RTID").append("<option value=\"" + v.RTID + "\">" + v.RTypeName + "</option>");
            });
        }, "json");
        var row = $('#dg').datagrid('getSelected');
        if (row) {
            $('#dlg').dialog('open').dialog('setTitle', '编辑权限');
            $('#fm').form('load', row);
            operation.url = 'Edit?RID=' + row.RID;
            $.get(operation.url, null, function (data) {
                $('#fm').form('load', data);
            });
        }
        else {
            $.messager.alert("系统提示","请选择一项编辑!","info");
        }
    },
    saveRight: function () {
        $('#fm').form('submit', {
            url: operation.url,
            onSubmit: function () {
                return $(this).form('validate');
            },
            success: function (result) {
                var result = eval('(' + result + ')');
                if (result.errorMsg) {
                    $.messager.show({
                        title: 'Error',
                        msg: result.errorMsg
                    });
                } else {
                    $('#dlg').dialog('close');        // close the dialog
                    $('#dg').datagrid('reload');    // reload the user data
                }
            }
        });
    },
    delRights: function () {
        var row = $('#dg').datagrid('getSelected');
        if (row) {
            $.messager.confirm('Confirm', '是否删除该权限?', function (r) {
                if (r) {
                    $.post('Delete', { RID: row.RID }, function (result) {
                        if (result.success) {
                            $('#dg').datagrid('reload');    // reload the user data
                        } else {
                            $.messager.show({    // show error message
                                title: 'Error',
                                msg: result.errorMsg
                            });
                        }
                    }, 'json');
                }
            });
        }
        else {
            $.messager.alert("系统提示", "请选择删除的行!", "info")
        }
    },
    loadRightTypes: function () {
        var params = {
            url: "/Admin/RightTypes/GetRightTypes",
            checkbox: false,
            lines: true,
            onClick: function (node) {
                $("#dg").datagrid({ url: "/Rights?page=1&rows=10&RTID=" + node.id });
            }
        };
        $("#content").tree(params);
    }
}
