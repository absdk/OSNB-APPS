//-----------------------------------------------------
//start Add, Edit, Delete - Success Funtion
// Add User Success Function
function AddUserSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#addUserDialog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("User added successfully.", "dialogSuccess");

        userObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Edit User Success Function
function EditUserSuccess() {
    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#editUserDialog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("User edited successfully.", "dialogSuccess");

        userObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Delete User Success Function
function DeleteUserSuccess() {
    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#deleteUserDailog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("User deleted successfully.", "dialogSuccess");

        userObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();

    }
}

// AssignRole User Success Function
function AssignRoleUserSuccess() {
    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#assignRoleUserDailog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("Assign role successfully.", "dialogSuccess");

        userObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();

    }
}

//end Add, Edit, Delete - Success Funtion
//-----------------------------------------------------

var userObjData;

$(function () {
    //start DataTable Script

    //for display more collapse data from product
    $('#userDataTable tbody td .userPro').on('click', 'img', function () {

        if ($(this).attr('class').match('catPro')) {
            var nTr = this.parentNode.parentNode;
            if (this.src.match('details_close')) {
                this.src = "/Content/Images/App/details_open.png";
                userObjData.fnClose(nTr);
            }
            else {
                this.src = "/Content/Images/App/details_close.png";
                var id = $(this).attr("rel");
                $.get("/Admin/User/GetRoles?userId=" + id, function (datas) {
                    userObjData.fnOpen(nTr, datas, 'details');
                });
            }
        }

    });

    userObjData = $('#userDataTable').dataTable({
        "bJQueryUI": false,
        "bAutoWidth": false,
        "sPaginationType": "bootstrap",
        "bSort": false,
        "oLanguage": {
            "sLengthMenu": "_MENU_ items per page",
            "sZeroRecords": "Data not found",
            "sProcessing": "Loading...",
            "sInfo": "_START_ - _END_ of _TOTAL_ items",
            "sInfoEmpty": "0 - 0 of 0 items",
            "sInfoFiltered": "(filtered from _MAX_ total items)"
        },
        "bProcessing": true,
        "bServerSide": true,
        "sAjaxSource": "/Admin/User/GetUsers",
        "aoColumns": [{ "sName": "Roles",
            "bSearchable": false,
            "bSortable": false,
            "fnRender": function (oObj) {
                return '<img class="userPro img-expand-collapse" src="/Content/Images/App/details_open.png" title="User List" alt="expand/collapse" rel="' +
                                oObj.aData[0] + '"/>';

            }

        },
            { "sName": "UserName" },
            { "sName": "Email" },
            { "sName": "IsApproved" },
            { "sName": "Actions",
                "bSearchable": false,
                "bSortable": false,
                "sWidth": "160px",
                "fnRender": function (oObj) {
                    return '<a class="lnkDetailsUser btn btn-primary btn-mini" style="margin-right: 5px;" href=\"/Admin/User/Details/' +
                                oObj.aData[4] + '\" ><icon class="icon-search icon-white"></icon></a>' +
                                '<a class="lnkEditUser  btn btn-success btn-mini" style="margin-right: 5px;" href=\"/Admin/User/Edit/' +
                                oObj.aData[4] + '\" ><icon class="icon-pencil icon-white"></icon></a>' +
                                '<a class="lnkDeleteUser btn btn-danger btn-mini" style="margin-right: 5px;" href=\"/Admin/User/Delete/' +
                                oObj.aData[4] + '\" ><icon class="icon-trash icon-white"></icon></a>' +
                                '<a class="lnkAssignRoleUser btn btn-success btn-mini" style="margin-right: 5px;" href=\"/Admin/User/AssignRole/' +
                                oObj.aData[4] + '\" ><icon class="icon-check icon-white"></icon></a>' +
                                '<a class="lnkAddMember btn btn-success btn-mini" href=\"/Admin/User/AddMember/' +
                                oObj.aData[4] + '\" ><icon class="icon-plus icon-white"></icon></a>';

                }

            }
            ]
    });

    //end DataTable Script

    //-------------------------------------------------------
    //start Add, Edit, Delete - Dialog, Click Event

    $("#addUserDialog").dialog({
        autoOpen: false,
        width: 500,
        resizable: false,
        modal: true,
        buttons: {
            "Add": function () {
                //make sure there is nothing on the message before we continue 
                $("#updateTargetId").html('');
                $("#addUserForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    //add User
    $('#lnkAddUser').click(function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#addUserDialog');
        var viewUrl = linkObj.attr('href');

        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#addUserForm");
            // Unbind existing validation
            $form.unbind();
            $form.data("validator", null);
            // Check document for changes
            $.validator.unobtrusive.parse(document);
            // Re add validation with changes
            $form.validate($form.data("unobtrusiveValidation").options);
            //open dialog
            dialogDiv.dialog('open');
        });
        return false;

    });

    //edit User
    $("#editUserDialog").dialog({
        autoOpen: false,
        width: 500,
        resizable: false,
        closeOnEscape: false,
        modal: true,
        close: function (event, ui) {
            $(".popover").hide();
        },
        buttons: {
            "Edit": function () {
                //make sure there is nothing on the message before we continue   
                $("#updateTargetId").html('');
                $("#editUserForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }

    });

    $('#userDataTable tbody td a.lnkEditUser').live('click', function () {
        //$('#userDataTable tbody td .lnkEditUser').on('click', 'a', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#editUserDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#editUserForm");
            // Unbind existing validation
            $form.unbind();
            $form.data("validator", null);
            // Check document for changes
            $.validator.unobtrusive.parse(document);
            // Re add validation with changes
            $form.validate($form.data("unobtrusiveValidation").options);
            //open dialog
            dialogDiv.dialog('open');
        });
        return false;

    });

    //delete User
    $("#deleteUserDailog").dialog({
        autoOpen: false,
        width: 400,
        resizable: false,
        modal: true,
        buttons: {
            "Yes": function () {
                //make sure there is nothing on the message before we continue                         
                $("#updateTargetId").html('');
                $("#deleteUserForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    $('#userDataTable tbody td a.lnkDeleteUser').live('click', function () {
        //$('#userDataTable tbody td .lnkDeleteUser').on('click', 'a', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#deleteUserDailog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#deleteUserForm");
            // Unbind existing validation
            $form.unbind();
            $form.data("validator", null);
            // Check document for changes
            $.validator.unobtrusive.parse(document);
            // Re add validation with changes
            $form.validate($form.data("unobtrusiveValidation").options);
            //open dialog
            dialogDiv.dialog('open');
        });
        return false;

    });

    //For details User
    $("#detailsUserDialog").dialog({
        autoOpen: false,
        width: 500,
        resizable: false,
        modal: true,
        buttons: {
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    $('#userDataTable tbody td a.lnkDetailsUser').live('click', function () {
        //$('#userDataTable tbody td .lnkDetailsUser').on('click', 'a', function () {

        var linkObj = $(this);
        var dialogDiv = $('#detailsUserDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            dialogDiv.dialog('open');
        });
        return false;

    });

    //Assign Role
    $("#assignRoleUserDailog").dialog({
        autoOpen: false,
        width: 400,
        resizable: false,
        modal: true,
        buttons: {
            "Ok": function () {
                //make sure there is nothing on the message before we continue                         
                $("#updateTargetId").html('');
                $("#assignRoleUserForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    $('#userDataTable tbody td a.lnkAssignRoleUser').live('click', function () {
        //$('#userDataTable tbody td .lnkAssignRoleUser').on('click', 'a', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#assignRoleUserDailog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {

            console.log(data);

            dialogDiv.html(data);
            //validation
            var $form = $("#assignRoleUserForm");
            // Unbind existing validation
            $form.unbind();
            $form.data("validator", null);
            // Check document for changes
            $.validator.unobtrusive.parse(document);
            // Re add validation with changes
            $form.validate($form.data("unobtrusiveValidation").options);
            //open dialog
            dialogDiv.dialog('open');

        });
        return false;

    });


    //end Add, Edit, Delete - Dialog, Click Event
    //-------------------------------------------------------

});