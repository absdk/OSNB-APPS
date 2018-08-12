//-----------------------------------------------------
//start Add, Edit, Delete - Success Funtion
// Add MemberHospital Success Function
function AddMemberHospitalSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#addMemberHospitalDialog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("Hospital saved successfully.", "dialogSuccess");

        memberHospitalObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Edit MemberHospital Success Function
function EditMemberHospitalSuccess() {
    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#editMemberHospitalDialog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("Hospital updated successfully.", "dialogSuccess");

        memberHospitalObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Delete MemberHospital Success Function
function DeleteMemberHospitalSuccess() {
    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#deleteMemberHospitalDailog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("Hospital deleted successfully.", "dialogSuccess");

        memberHospitalObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();

    }
}
//end Add, Edit, Delete - Success Funtion
//-----------------------------------------------------

var memberHospitalObjData;

$(function () {
    //start DataTable Script

    memberHospitalObjData = $('#memberHospitalDataTable').dataTable({
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
        "sAjaxSource": "/Admin/MemberHospital/GetMemberHospitals",
        "aoColumns": [
            { "sName": "HospitalName" },
            { "sName": "Address" },
            { "sName": "LocationX" },
            { "sName": "LocationY" },
            { "sName": "MemberZoneName" },
            { "sName": "Actions",
                "bSearchable": false,
                "bSortable": false,
                "sWidth": "90px",
                "fnRender": function (oObj) {
                    return '<a class="lnkDetailsMemberHospital btn btn-primary btn-mini" style="margin-right: 5px;" href=\"/Admin/MemberHospital/Details/' +
                                oObj.aData[5] + '\" ><icon class="icon-search icon-white"></icon></a>' +
                                '<a class="lnkEditMemberHospital  btn btn-success btn-mini" style="margin-right: 5px;" href=\"/Admin/MemberHospital/Edit/' +
                                oObj.aData[5] + '\" ><icon class="icon-pencil icon-white"></icon></a>' +
                                '<a class="lnkDeleteMemberHospital btn btn-danger btn-mini" href=\"/Admin/MemberHospital/Delete/' +
                                oObj.aData[5] + '\" ><icon class="icon-trash icon-white"></icon></a>';

                }

            }
            ]
    });

    //end DataTable Script

    //-------------------------------------------------------
    //start Add, Edit, Delete - Dialog, Click Event

    $("#addMemberHospitalDialog").dialog({
        autoOpen: false,
        width: 500,
        resizable: false,
        modal: true,
        buttons: {
            "Add": function () {
                //make sure there is nothing on the message before we continue 
                $("#updateTargetId").html('');
                $("#addMemberHospitalForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    //add MemberHospital
    $('#lnkAddMemberHospital').click(function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#addMemberHospitalDialog');
        var viewUrl = linkObj.attr('href');

        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#addMemberHospitalForm");
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

    //edit MemberHospital
    $("#editMemberHospitalDialog").dialog({
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
                $("#editMemberHospitalForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }

    });

    $('#memberHospitalDataTable tbody td a.lnkEditMemberHospital').live('click', function () {
        //$('#memberHospitalDataTable tbody td .lnkEditMemberHospital').on('click', 'a', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#editMemberHospitalDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#editMemberHospitalForm");
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

    //delete MemberHospital
    $("#deleteMemberHospitalDailog").dialog({
        autoOpen: false,
        width: 400,
        resizable: false,
        modal: true,
        buttons: {
            "Yes": function () {
                //make sure there is nothing on the message before we continue                         
                $("#updateTargetId").html('');
                $("#deleteMemberHospitalForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    $('#memberHospitalDataTable tbody td a.lnkDeleteMemberHospital').live('click', function () {
        //$('#memberHospitalDataTable tbody td .lnkDeleteMemberHospital').on('click', 'a', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#deleteMemberHospitalDailog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#deleteMemberHospitalForm");
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

    //For details MemberHospital
    $("#detailsMemberHospitalDialog").dialog({
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

    $('#memberHospitalDataTable tbody td a.lnkDetailsMemberHospital').live('click', function () {
        //$('#memberHospitalDataTable tbody td .lnkDetailsMemberHospital').on('click', 'a', function () {

        var linkObj = $(this);
        var dialogDiv = $('#detailsMemberHospitalDialog');
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