$(document).ready(function () {
    operation.init();
});
var operation = {
    url: "",
    init: function () {
        $("#add_").click(this.addRole);
        $("#edit_").click(this.editRole);
        $("#delete_").click(this.deleteRole);
        $("#btn_Confirm_").click(this.saveRole);
    },
    addRole: function () {
        $('#dlg').dialog('open').dialog('setTitle', '新建角色');
        $('.fm').form('clear');
        operation.url = "Create";
    },
    editRole: function () {
        var row = $("#dg").datagrid("getSelected");
        if (row) {
            $('#dlg').dialog('open').dialog('setTitle', '编辑权限');
            $('#fm').form('load', row);
            operation.url = 'Edit?RoleID=' + row.RoleID;
            $.get(operation.url, null, function (data) {
                $('#fm').form('load', data);
            });
        }
        else {
            $.messager.alert("请选择一项编辑!");
        }
    },
    deleteRole: function () {
        var row = $('#dg').datagrid('getSelected');
        if (row) {
            $.messager.confirm('Confirm', '是否删除该权限?', function (r) {
                if (r) {
                    $.post('Delete', { RoleID: row.RoleID }, function (result) {
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
    saveRole: function () {
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
    }
}
