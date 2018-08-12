//-----------------------------------------------------
//start Add, Edit, Delete - Success Funtion
// Add MemberZone Success Function
function AddMemberZoneSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#addMemberZoneDialog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("Zone saved successfully.", "dialogSuccess");

        memberZoneObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Edit MemberZone Success Function
function EditMemberZoneSuccess() {
    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#editMemberZoneDialog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("Zone updated successfully.", "dialogSuccess");

        memberZoneObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Delete MemberZone Success Function
function DeleteMemberZoneSuccess() {
    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#deleteMemberZoneDailog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("Zone deleted successfully.", "dialogSuccess");

        memberZoneObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();

    }
}
//end Add, Edit, Delete - Success Funtion
//-----------------------------------------------------

var memberZoneObjData;

$(function () {
    //start DataTable Script

    memberZoneObjData = $('#memberZoneDataTable').dataTable({
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
        "sAjaxSource": "/Admin/MemberZone/GetMemberZones",
        "aoColumns": [
            { "sName": "ZoneName" },
            { "sName": "LocationX" },
            { "sName": "LocationY" },
            { "sName": "MemberDistrictName" },
            { "sName": "Actions",
                "bSearchable": false,
                "bSortable": false,
                "sWidth": "90px",
                "fnRender": function (oObj) {
                    return '<a class="lnkDetailsMemberZone btn btn-primary btn-mini" style="margin-right: 5px;" href=\"/Admin/MemberZone/Details/' +
                                oObj.aData[4] + '\" ><icon class="icon-search icon-white"></icon></a>' +
                                '<a class="lnkEditMemberZone  btn btn-success btn-mini" style="margin-right: 5px;" href=\"/Admin/MemberZone/Edit/' +
                                oObj.aData[4] + '\" ><icon class="icon-pencil icon-white"></icon></a>' +
                                '<a class="lnkDeleteMemberZone btn btn-danger btn-mini" href=\"/Admin/MemberZone/Delete/' +
                                oObj.aData[4] + '\" ><icon class="icon-trash icon-white"></icon></a>';

                }

            }
            ]
    });

    //end DataTable Script

    //-------------------------------------------------------
    //start Add, Edit, Delete - Dialog, Click Event

    $("#addMemberZoneDialog").dialog({
        autoOpen: false,
        width: 500,
        resizable: false,
        modal: true,
        buttons: {
            "Add": function () {
                //make sure there is nothing on the message before we continue 
                $("#updateTargetId").html('');
                $("#addMemberZoneForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    //add MemberZone
    $('#lnkAddMemberZone').click(function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#addMemberZoneDialog');
        var viewUrl = linkObj.attr('href');

        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#addMemberZoneForm");
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

    //edit MemberZone
    $("#editMemberZoneDialog").dialog({
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
                $("#editMemberZoneForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }

    });

    $('#memberZoneDataTable tbody td a.lnkEditMemberZone').live('click', function () {
        //$('#memberZoneDataTable tbody td .lnkEditMemberZone').on('click', 'a', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#editMemberZoneDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#editMemberZoneForm");
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

    //delete MemberZone
    $("#deleteMemberZoneDailog").dialog({
        autoOpen: false,
        width: 400,
        resizable: false,
        modal: true,
        buttons: {
            "Yes": function () {
                //make sure there is nothing on the message before we continue                         
                $("#updateTargetId").html('');
                $("#deleteMemberZoneForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    $('#memberZoneDataTable tbody td a.lnkDeleteMemberZone').live('click', function () {
        //$('#memberZoneDataTable tbody td .lnkDeleteMemberZone').on('click', 'a', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#deleteMemberZoneDailog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#deleteMemberZoneForm");
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

    //For details MemberZone
    $("#detailsMemberZoneDialog").dialog({
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

    $('#memberZoneDataTable tbody td a.lnkDetailsMemberZone').live('click', function () {
        //$('#memberZoneDataTable tbody td .lnkDetailsMemberZone').on('click', 'a', function () {

        var linkObj = $(this);
        var dialogDiv = $('#detailsMemberZoneDialog');
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