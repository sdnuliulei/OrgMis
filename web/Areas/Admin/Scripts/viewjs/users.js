$(document).ready(function () {
    operation.init();
});
var operation = {
    url: "",
    init: function () {
        this.loadOrgs();
        $("#add_").click(this.addUser);
        $("#edit_").click(this.editUser);
        $("#delete_").click(this.delUser);
        $("#btn_Confirm_").click(this.saveUser);
    },
    loadOrgs: function () {
        var params = {
            url: "/Admin/Orgs/Index?root=NBJZ4R",
            checkbox: false,
            lines: true,
            onClick: function (node) {
                $("#dg").datagrid({ url: "?Org_id=" + node.id, page: 1, rows: 10 });
            }
        };
        $("#content").tree(params);
        $("#Org_id").combotree({
            url: params.url,
            required: true
        });
    },
    loadRoles: function () {
        $.post("/Admin/Roles/List", null, function (data) {
            $.each(data, function (k, v) {
                $("#RoleID").append("<option value='" + v.RoleID + "'>" + v.RoleName + "</option>");
            });
        });
    },
    addUser: function () {
        $('#dlg').dialog('open').dialog('setTitle', '新建用户');
        $('.fm').form('clear');
        operation.url = "Create";
        operation.loadRoles();
    },
    editUser: function () {
        $('#dlg').dialog('open').dialog('setTitle', '编辑用户');
        $('.fm').form('clear');
        operation.loadRoles();
        var row = $('#dg').datagrid('getSelected');
        if (row) {
            $.get("Edit", { UserID: row.UserID }, function (data) {
                $('#fm').form('load', data);
                operation.url = "Edit?UserID=" + row.UserID;
            });
        }
        else {
            $.messager.alert("系统提示", "请选择一项编辑!", "info");
        }
    },
    saveUser: function () {
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
    delUser: function () {
        var row = $("#dg").datagrid("getSelected");
        if (row) {
            $.messager.confirm("确认?", "是否删除?", function (r) {
                if (r) {
                    $.post("Delete", { UserID: row.UserID }, function (result) {
                        if (result.success) {
                            $('#dg').datagrid('reload');    // reload the user data
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
            $.messager.alert("系统提示", "请选择一项删除!", "info");
        }
    }
};

$.extend($.fn.validatebox.defaults.rules, {
    equals: {
        validator: function (value, param) {
            return value == $(param[0]).val();
        },
        message: '密码确认不一致!'
    },
    isUserSingle: {
        validator: function (value, param) {
            return true;
        },
        message: '该用户已存在!'
    },
    minLength: {
        validator: function (value, param) {
            return value.length >= param[0];
        },
        message: '输入至少{0}个字符!'
    }
});
