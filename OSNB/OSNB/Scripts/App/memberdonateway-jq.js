//-----------------------------------------------------
//start Add, Edit, Delete - Success Funtion
// Add MemberDonateWay Success Function
function AddMemberDonateWaySuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#addMemberDonateWayDialog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("Donate Way saved successfully.", "dialogSuccess");

        memberDonateWayObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Edit MemberDonateWay Success Function
function EditMemberDonateWaySuccess() {
    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#editMemberDonateWayDialog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("Donate Way updated successfully.", "dialogSuccess");

        memberDonateWayObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Delete MemberDonateWay Success Function
function DeleteMemberDonateWaySuccess() {
    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#deleteMemberDonateWayDailog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("Donate Way deleted successfully.", "dialogSuccess");

        memberDonateWayObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();

    }
}
//end Add, Edit, Delete - Success Funtion
//-----------------------------------------------------

var memberDonateWayObjData;

$(function () {
    //start DataTable Script

    memberDonateWayObjData = $('#memberDonateWayDataTable').dataTable({
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
        "sAjaxSource": "/Admin/MemberDonateWay/GetMemberDonateWays",
        "aoColumns": [
            { "sName": "DonateWayName" },
            { "sName": "Actions",
                "bSearchable": false,
                "bSortable": false,
                "sWidth": "90px",
                "fnRender": function (oObj) {
                    return '<a class="lnkDetailsMemberDonateWay btn btn-primary btn-mini" style="margin-right: 5px;" href=\"/Admin/MemberDonateWay/Details/' +
                                oObj.aData[1] + '\" ><icon class="icon-search icon-white"></icon></a>' +
                                '<a class="lnkEditMemberDonateWay  btn btn-success btn-mini" style="margin-right: 5px;" href=\"/Admin/MemberDonateWay/Edit/' +
                                oObj.aData[1] + '\" ><icon class="icon-pencil icon-white"></icon></a>' +
                                '<a class="lnkDeleteMemberDonateWay btn btn-danger btn-mini" href=\"/Admin/MemberDonateWay/Delete/' +
                                oObj.aData[1] + '\" ><icon class="icon-trash icon-white"></icon></a>';

                }

            }
            ]
    });

    //end DataTable Script

    //-------------------------------------------------------
    //start Add, Edit, Delete - Dialog, Click Event

    $("#addMemberDonateWayDialog").dialog({
        autoOpen: false,
        width: 500,
        resizable: false,
        modal: true,
        buttons: {
            "Add": function () {
                //make sure there is nothing on the message before we continue 
                $("#updateTargetId").html('');
                $("#addMemberDonateWayForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    //add MemberDonateWay
    $('#lnkAddMemberDonateWay').click(function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#addMemberDonateWayDialog');
        var viewUrl = linkObj.attr('href');

        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#addMemberDonateWayForm");
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

    //edit MemberDonateWay
    $("#editMemberDonateWayDialog").dialog({
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
                $("#editMemberDonateWayForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }

    });

    $('#memberDonateWayDataTable tbody td a.lnkEditMemberDonateWay').live('click', function () {
        //$('#memberDonateWayDataTable tbody td .lnkEditMemberDonateWay').on('click', 'a', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#editMemberDonateWayDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#editMemberDonateWayForm");
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

    //delete MemberDonateWay
    $("#deleteMemberDonateWayDailog").dialog({
        autoOpen: false,
        width: 400,
        resizable: false,
        modal: true,
        buttons: {
            "Yes": function () {
                //make sure there is nothing on the message before we continue                         
                $("#updateTargetId").html('');
                $("#deleteMemberDonateWayForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    $('#memberDonateWayDataTable tbody td a.lnkDeleteMemberDonateWay').live('click', function () {
        //$('#memberDonateWayDataTable tbody td .lnkDeleteMemberDonateWay').on('click', 'a', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#deleteMemberDonateWayDailog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#deleteMemberDonateWayForm");
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

    //For details MemberDonateWay
    $("#detailsMemberDonateWayDialog").dialog({
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

    $('#memberDonateWayDataTable tbody td a.lnkDetailsMemberDonateWay').live('click', function () {
        //$('#memberDonateWayDataTable tbody td .lnkDetailsMemberDonateWay').on('click', 'a', function () {

        var linkObj = $(this);
        var dialogDiv = $('#detailsMemberDonateWayDialog');
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