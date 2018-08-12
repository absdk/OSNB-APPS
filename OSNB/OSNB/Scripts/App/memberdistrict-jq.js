//-----------------------------------------------------
//start Add, Edit, Delete - Success Funtion
// Add MemberDistrict Success Function
function AddMemberDistrictSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#addMemberDistrictDialog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("District saved successfully.", "dialogSuccess");

        memberDistrictObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Edit MemberDistrict Success Function
function EditMemberDistrictSuccess() {
    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#editMemberDistrictDialog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("District updated successfully.", "dialogSuccess");

        memberDistrictObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Delete MemberDistrict Success Function
function DeleteMemberDistrictSuccess() {
    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#deleteMemberDistrictDailog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("District deleted successfully.", "dialogSuccess");

        memberDistrictObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();

    }
}
//end Add, Edit, Delete - Success Funtion
//-----------------------------------------------------

var memberDistrictObjData;

$(function () {
    //start DataTable Script

    //for display more collapse data from product
    $('#memberDistrictDataTable tbody tr td img.memberDistrictPro').on('click', function () {

        console.log("hi");

        if ($(this).attr('class').match('memberDistrictPro')) {
            var nTr = this.parentNode.parentNode;
            if (this.src.match('details_close')) {
                this.src = "/Content/Images/App/details_open.png";
                memberDistrictObjData.fnClose(nTr);
            }
            else {
                this.src = "/Content/Images/App/details_close.png";
                var id = $(this).attr("rel");
                $.get("/Admin/MemberDistrict/GetZones?zoneId=" + id, function (datas) {
                    memberDistrictObjData.fnOpen(nTr, datas, 'details');
                });
            }
        }

    });

    memberDistrictObjData = $('#memberDistrictDataTable').dataTable({
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
        "sAjaxSource": "/Admin/MemberDistrict/GetMemberDistricts",
        "aoColumns": [{ "sName": "Zones",
            "bSearchable": false,
            "bSortable": false,
            "fnRender": function (oObj) {
                return '<img class="memberDistrictPro img-expand-collapse" src="/Content/Images/App/details_open.png" title="Member Zone List" alt="expand/collapse" rel="' +
                                oObj.aData[0] + '"/>';

            }

        },
            { "sName": "DistrictName" },
            { "sName": "Actions",
                "bSearchable": false,
                "bSortable": false,
                "sWidth": "90px",
                "fnRender": function (oObj) {
                    return '<a class="lnkDetailsMemberDistrict btn btn-primary btn-mini" style="margin-right: 5px;" href=\"/Admin/MemberDistrict/Details/' +
                                oObj.aData[2] + '\" ><icon class="icon-search icon-white"></icon></a>' +
                                '<a class="lnkEditMemberDistrict  btn btn-success btn-mini" style="margin-right: 5px;" href=\"/Admin/MemberDistrict/Edit/' +
                                oObj.aData[2] + '\" ><icon class="icon-pencil icon-white"></icon></a>' +
                                '<a class="lnkDeleteMemberDistrict btn btn-danger btn-mini" href=\"/Admin/MemberDistrict/Delete/' +
                                oObj.aData[2] + '\" ><icon class="icon-trash icon-white"></icon></a>';

                }

            }
            ]
    });

    //end DataTable Script

    //-------------------------------------------------------
    //start Add, Edit, Delete - Dialog, Click Event

    $("#addMemberDistrictDialog").dialog({
        autoOpen: false,
        width: 500,
        resizable: false,
        modal: true,
        buttons: {
            "Add": function () {
                //make sure there is nothing on the message before we continue 
                $("#updateTargetId").html('');
                $("#addMemberDistrictForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    //add MemberDistrict
    $('#lnkAddMemberDistrict').click(function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#addMemberDistrictDialog');
        var viewUrl = linkObj.attr('href');

        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#addMemberDistrictForm");
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

    //edit MemberDistrict
    $("#editMemberDistrictDialog").dialog({
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
                $("#editMemberDistrictForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }

    });

    $('#memberDistrictDataTable tbody td a.lnkEditMemberDistrict').live('click', function () {
    //$('#memberDistrictDataTable tbody td .lnkEditMemberDistrict').on('click', 'a', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#editMemberDistrictDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#editMemberDistrictForm");
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

    //delete MemberDistrict
    $("#deleteMemberDistrictDailog").dialog({
        autoOpen: false,
        width: 400,
        resizable: false,
        modal: true,
        buttons: {
            "Yes": function () {
                //make sure there is nothing on the message before we continue                         
                $("#updateTargetId").html('');
                $("#deleteMemberDistrictForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    $('#memberDistrictDataTable tbody td a.lnkDeleteMemberDistrict').live('click', function () {
    //$('#memberDistrictDataTable tbody td .lnkDeleteMemberDistrict').on('click', 'a', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#deleteMemberDistrictDailog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#deleteMemberDistrictForm");
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

    //For details MemberDistrict
    $("#detailsMemberDistrictDialog").dialog({
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

    $('#memberDistrictDataTable tbody td a.lnkDetailsMemberDistrict').live('click', function () {
        //$('#memberDistrictDataTable tbody td .lnkDetailsMemberDistrict').on('click', 'a', function () {

        var linkObj = $(this);
        var dialogDiv = $('#detailsMemberDistrictDialog');
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