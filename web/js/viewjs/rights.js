var url;
function newUser() {
    $('#dlg').dialog('open').dialog('setTitle', '新建权限');
    $('.fm').form('clear');
    url = "/Rights/Create";
    $.post("/RightTypes/Index", null, function (data) {
        $("#RTID").html("");
        $.each(data, function (k, v) {
            $("#RTID").append("<option value=\"" + v.RTID + "\">" + v.RTypeName + "</option>");
        });
    }, "json");
}
function editUser() {
    $.post("/RightTypes/Index", null, function (data) {
        $("#RTID").html("");
        $.each(data, function (k, v) {
            $("#RTID").append("<option value=\"" + v.RTID + "\">" + v.RTypeName + "</option>");
        });
    }, "json");
    var row = $('#dg').datagrid('getSelected');
    if (row) {
        $('#dlg').dialog('open').dialog('setTitle', '编辑权限');
        $('#fm').form('load', row);
        url = '/Rights/Edit?RID=' + row.RID;
        $.get(url, null, function (data) { 
        
        });
    }
}
function saveUser() {
    $('#fm').form('submit', {
        url: url,
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
function destroyUser() {
    var row = $('#dg').datagrid('getSelected');
    if (row) {
        $.messager.confirm('Confirm', '是否删除该权限?', function (r) {
            if (r) {
                $.post('/Rights/Delete', { RID: row.RID }, function (result) {
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
}

//页面加载
$(document).ready(function () {

});
var operation = {
    init: function () {

    }, //初始化
    addRight: function () {

    },
    editRight: function () {

    },
    delRights: function () {

    },
    loadRightTypes: function () { 
    
    }
}
