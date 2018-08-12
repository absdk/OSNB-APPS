//-----------------------------------------------------
//start Add, Edit, Delete - Success Funtion
// Add MemberStatus Success Function
function AddMemberStatusSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#addMemberStatusDialog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("Status saved successfully.", "dialogSuccess");

        memberStatusObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Edit MemberStatus Success Function
function EditMemberStatusSuccess() {
    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#editMemberStatusDialog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("Status updated successfully.", "dialogSuccess");

        memberStatusObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Delete MemberStatus Success Function
function DeleteMemberStatusSuccess() {
    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#deleteMemberStatusDailog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("Status deleted successfully.", "dialogSuccess");

        memberStatusObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();

    }
}
//end Add, Edit, Delete - Success Funtion
//-----------------------------------------------------

var memberStatusObjData;

$(function () {
    //start DataTable Script

    memberStatusObjData = $('#memberStatusDataTable').dataTable({
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
        "sAjaxSource": "/Admin/MemberStatus/GetMemberStatues",
        "aoColumns": [
            { "sName": "MemberStatusTitle" },
            { "sName": "MemberStatusDescription" },
            { "sName": "MemberStatusIcon" },
            { "sName": "Actions",
                "bSearchable": false,
                "bSortable": false,
                "sWidth": "90px",
                "fnRender": function (oObj) {
                    return '<a class="lnkDetailsMemberStatus btn btn-primary btn-mini" style="margin-right: 5px;" href=\"/Admin/MemberStatus/Details/' +
                                oObj.aData[3] + '\" ><icon class="icon-search icon-white"></icon></a>' +
                                '<a class="lnkEditMemberStatus  btn btn-success btn-mini" style="margin-right: 5px;" href=\"/Admin/MemberStatus/Edit/' +
                                oObj.aData[3] + '\" ><icon class="icon-pencil icon-white"></icon></a>' +
                                '<a class="lnkDeleteMemberStatus btn btn-danger btn-mini" href=\"/Admin/MemberStatus/Delete/' +
                                oObj.aData[3] + '\" ><icon class="icon-trash icon-white"></icon></a>';

                }

            }
            ]
    });

    //end DataTable Script

    //-------------------------------------------------------
    //start Add, Edit, Delete - Dialog, Click Event

    $("#addMemberStatusDialog").dialog({
        autoOpen: false,
        width: 500,
        resizable: false,
        modal: true,
        buttons: {
            "Add": function () {
                //make sure there is nothing on the message before we continue 
                $("#updateTargetId").html('');
                $("#addMemberStatusForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    //add MemberStatus
    $('#lnkAddMemberStatus').click(function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#addMemberStatusDialog');
        var viewUrl = linkObj.attr('href');

        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#addMemberStatusForm");
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

    //edit MemberStatus
    $("#editMemberStatusDialog").dialog({
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
                $("#editMemberStatusForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }

    });

    $('#memberStatusDataTable tbody td a.lnkEditMemberStatus').live('click', function () {
        //$('#memberStatusDataTable tbody td .lnkEditMemberStatus').on('click', 'a', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#editMemberStatusDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#editMemberStatusForm");
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

    //delete MemberStatus
    $("#deleteMemberStatusDailog").dialog({
        autoOpen: false,
        width: 400,
        resizable: false,
        modal: true,
        buttons: {
            "Yes": function () {
                //make sure there is nothing on the message before we continue                         
                $("#updateTargetId").html('');
                $("#deleteMemberStatusForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    $('#memberStatusDataTable tbody td a.lnkDeleteMemberStatus').live('click', function () {
        //$('#memberStatusDataTable tbody td .lnkDeleteMemberStatus').on('click', 'a', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#deleteMemberStatusDailog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#deleteMemberStatusForm");
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

    //For details MemberStatus
    $("#detailsMemberStatusDialog").dialog({
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

    $('#memberStatusDataTable tbody td a.lnkDetailsMemberStatus').live('click', function () {
        //$('#memberStatusDataTable tbody td .lnkDetailsMemberStatus').on('click', 'a', function () {

        var linkObj = $(this);
        var dialogDiv = $('#detailsMemberStatusDialog');
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