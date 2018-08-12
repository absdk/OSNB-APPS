//-----------------------------------------------------
//start Add, Edit, Delete - Success Funtion
// Add MemberBloodGroup Success Function
function AddMemberBloodGroupSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#addMemberBloodGroupDialog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("Blood Group saved successfully.", "dialogSuccess");

        memberBloodGroupObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Edit MemberBloodGroup Success Function
function EditMemberBloodGroupSuccess() {
    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#editMemberBloodGroupDialog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("Blood Group updated successfully.", "dialogSuccess");

        memberBloodGroupObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Delete MemberBloodGroup Success Function
function DeleteMemberBloodGroupSuccess() {
    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#deleteMemberBloodGroupDailog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("Blood Group deleted successfully.", "dialogSuccess");

        memberBloodGroupObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();

    }
}
//end Add, Edit, Delete - Success Funtion
//-----------------------------------------------------

var memberBloodGroupObjData;

$(function () {
    //start DataTable Script

    memberBloodGroupObjData = $('#memberBloodGroupDataTable').dataTable({
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
        "sAjaxSource": "/Admin/MemberBloodGroup/GetMemberBloodGroups",
        "aoColumns": [
            { "sName": "BloodGroupName" },
            { "sName": "Actions",
                "bSearchable": false,
                "bSortable": false,
                "sWidth": "100px",
                "fnRender": function (oObj) {
                    return '<a class="lnkDetailsMemberBloodGroup btn btn-primary btn-mini" style="margin-right: 5px;" href=\"/Admin/MemberBloodGroup/Details/' +
                                oObj.aData[1] + '\" ><icon class="icon-search icon-white"></icon></a>' +
                                '<a class="lnkEditMemberBloodGroup  btn btn-success btn-mini" style="margin-right: 5px;" href=\"/Admin/MemberBloodGroup/Edit/' +
                                oObj.aData[1] + '\" ><icon class="icon-pencil icon-white"></icon></a>' +
                                '<a class="lnkDeleteMemberBloodGroup btn btn-danger btn-mini" href=\"/Admin/MemberBloodGroup/Delete/' +
                                oObj.aData[1] + '\" ><icon class="icon-trash icon-white"></icon></a>';

                }

            }
            ]
    });

    //end DataTable Script

    //-------------------------------------------------------
    //start Add, Edit, Delete - Dialog, Click Event

    $("#addMemberBloodGroupDialog").dialog({
        autoOpen: false,
        width: 500,
        resizable: false,
        modal: true,
        buttons: {
            "Add": function () {
                //make sure there is nothing on the message before we continue 
                $("#updateTargetId").html('');
                $("#addMemberBloodGroupForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    //add MemberBloodGroup
    $('#lnkAddMemberBloodGroup').click(function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#addMemberBloodGroupDialog');
        var viewUrl = linkObj.attr('href');

        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#addMemberBloodGroupForm");
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

    //edit MemberBloodGroup
    $("#editMemberBloodGroupDialog").dialog({
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
                $("#editMemberBloodGroupForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }

    });

    $('#memberBloodGroupDataTable tbody td a.lnkEditMemberBloodGroup').live('click', function () {
        //$('#memberBloodGroupDataTable tbody td .lnkEditMemberBloodGroup').on('click', 'a', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#editMemberBloodGroupDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#editMemberBloodGroupForm");
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

    //delete MemberBloodGroup
    $("#deleteMemberBloodGroupDailog").dialog({
        autoOpen: false,
        width: 400,
        resizable: false,
        modal: true,
        buttons: {
            "Yes": function () {
                //make sure there is nothing on the message before we continue                         
                $("#updateTargetId").html('');
                $("#deleteMemberBloodGroupForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    $('#memberBloodGroupDataTable tbody td a.lnkDeleteMemberBloodGroup').live('click', function () {
        //$('#memberBloodGroupDataTable tbody td .lnkDeleteMemberBloodGroup').on('click', 'a', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#deleteMemberBloodGroupDailog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#deleteMemberBloodGroupForm");
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

    //For details MemberBloodGroup
    $("#detailsMemberBloodGroupDialog").dialog({
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

    $('#memberBloodGroupDataTable tbody td a.lnkDetailsMemberBloodGroup').live('click', function () {
        //$('#memberBloodGroupDataTable tbody td .lnkDetailsMemberBloodGroup').on('click', 'a', function () {

        var linkObj = $(this);
        var dialogDiv = $('#detailsMemberBloodGroupDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            dialogDiv.dialog('open');
        });
        return false;

    });

    //end Add, Edit, Delete - Dialog, Click Event
    //-------------------------------------------------------

});