$(document).ready(function () {
    operation.init();
});

var operation = {
    url: "",
    init: function () {
        this.loadOrgs();
        $("#add_").click(this.addOrg);
        $("#edit_").click(this.editOrg);
        $("#del_").click(this.removeOrg);
        $("#btn_Confirm_").click(this.saveOrg);
    },
    loadOrgs: function () {
        var params = {
            url: "?root=NBJZ4R",
            checkbox: false,
            lines: true
        };
        $("#content").tree(params);
    },
    addOrg: function () {
        $('#dlg').dialog('open').dialog('setTitle', '新建部门');
        $('.fm').form('clear');
        var Org_pId = "NBJZ4R";
        var node = $('#content').tree('getSelected');
        if (node) {
            Org_pId = node.id;
        }
        $("#Org_pId").val(Org_pId);
        operation.url = "Create";
    },
    editOrg: function () {
        var node = $("#content").tree("getSelected");
        if (node) {
            $('#dlg').dialog('open').dialog('setTitle', '编辑部门');
            $('.fm').form('clear');
            $.get("Edit", { Org_id: node.id }, function (data) {
                $('#fm').form('load', data);
                operation.url = "Edit"
            });
        }
        else {
            $.messager.alert("系统提示", "请选择一项编辑", "info");
        }
    },
    removeOrg: function () {
        var node = $("#content").tree("getSelected");
        if (node) {
            $.messager.confirm('确认提示', "是否删除该部门?", function (r) {
                if (r) {
                    $.post("Delete", { Org_id: node.id }, function (result) {
                        if (result.success) {
                            $('#content').tree('reload');    // reload the user data
                        } else {
                            $.messager.show({    // show error message
                                title: 'Error',
                                msg: result.errorMsg
                            });
                        }
                    });
                }
            });
        }
        else {
            $.messager.alert("系统提示", "请选择一项删除", "info");
        }
    },
    saveOrg: function () {
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
                    $('#content').tree('reload');    // reload the user data
                }
            }
        });
    }
}