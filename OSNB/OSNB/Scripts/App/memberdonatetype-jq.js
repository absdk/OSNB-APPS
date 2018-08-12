//-----------------------------------------------------
//start Add, Edit, Delete - Success Funtion
// Add MemberDonateType Success Function
function AddMemberDonateTypeSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#addMemberDonateTypeDialog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("Donate Type saved successfully.", "dialogSuccess");

        memberDonateTypeObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Edit MemberDonateType Success Function
function EditMemberDonateTypeSuccess() {
    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#editMemberDonateTypeDialog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("Donate Type updated successfully.", "dialogSuccess");

        memberDonateTypeObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Delete MemberDonateType Success Function
function DeleteMemberDonateTypeSuccess() {
    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#deleteMemberDonateTypeDailog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("Donate Type deleted successfully.", "dialogSuccess");

        memberDonateTypeObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();

    }
}
//end Add, Edit, Delete - Success Funtion
//-----------------------------------------------------

var memberDonateTypeObjData;

$(function () {
    //start DataTable Script

    memberDonateTypeObjData = $('#memberDonateTypeDataTable').dataTable({
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
        "sAjaxSource": "/Admin/MemberDonateType/GetMemberDonateTypes",
        "aoColumns": [
            { "sName": "DonateTypeName" },
            { "sName": "Actions",
                "bSearchable": false,
                "bSortable": false,
                "sWidth": "90px",
                "fnRender": function (oObj) {
                    return '<a class="lnkDetailsMemberDonateType btn btn-primary btn-mini" style="margin-right: 5px;" href=\"/Admin/MemberDonateType/Details/' +
                                oObj.aData[1] + '\" ><icon class="icon-search icon-white"></icon></a>' +
                                '<a class="lnkEditMemberDonateType  btn btn-success btn-mini" style="margin-right: 5px;" href=\"/Admin/MemberDonateType/Edit/' +
                                oObj.aData[1] + '\" ><icon class="icon-pencil icon-white"></icon></a>' +
                                '<a class="lnkDeleteMemberDonateType btn btn-danger btn-mini" href=\"/Admin/MemberDonateType/Delete/' +
                                oObj.aData[1] + '\" ><icon class="icon-trash icon-white"></icon></a>';

                }

            }
            ]
    });

    //end DataTable Script

    //-------------------------------------------------------
    //start Add, Edit, Delete - Dialog, Click Event

    $("#addMemberDonateTypeDialog").dialog({
        autoOpen: false,
        width: 500,
        resizable: false,
        modal: true,
        buttons: {
            "Add": function () {
                //make sure there is nothing on the message before we continue 
                $("#updateTargetId").html('');
                $("#addMemberDonateTypeForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    //add MemberDonateType
    $('#lnkAddMemberDonateType').click(function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#addMemberDonateTypeDialog');
        var viewUrl = linkObj.attr('href');

        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#addMemberDonateTypeForm");
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

    //edit MemberDonateType
    $("#editMemberDonateTypeDialog").dialog({
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
                $("#editMemberDonateTypeForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }

    });

    $('#memberDonateTypeDataTable tbody td a.lnkEditMemberDonateType').live('click', function () {
        //$('#memberDonateTypeDataTable tbody td .lnkEditMemberDonateType').on('click', 'a', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#editMemberDonateTypeDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#editMemberDonateTypeForm");
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

    //delete MemberDonateType
    $("#deleteMemberDonateTypeDailog").dialog({
        autoOpen: false,
        width: 400,
        resizable: false,
        modal: true,
        buttons: {
            "Yes": function () {
                //make sure there is nothing on the message before we continue                         
                $("#updateTargetId").html('');
                $("#deleteMemberDonateTypeForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    $('#memberDonateTypeDataTable tbody td a.lnkDeleteMemberDonateType').live('click', function () {
        //$('#memberDonateTypeDataTable tbody td .lnkDeleteMemberDonateType').on('click', 'a', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#deleteMemberDonateTypeDailog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#deleteMemberDonateTypeForm");
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

    //For details MemberDonateType
    $("#detailsMemberDonateTypeDialog").dialog({
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

    $('#memberDonateTypeDataTable tbody td a.lnkDetailsMemberDonateType').live('click', function () {
        //$('#memberDonateTypeDataTable tbody td .lnkDetailsMemberDonateType').on('click', 'a', function () {

        var linkObj = $(this);
        var dialogDiv = $('#detailsMemberDonateTypeDialog');
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