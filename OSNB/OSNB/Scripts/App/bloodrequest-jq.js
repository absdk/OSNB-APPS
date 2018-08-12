//-----------------------------------------------------
//start Add, Edit, Delete - Success Funtion

var bloodRequestObjData;

$(function () {
    //start DataTable Script

    bloodRequestObjData = $('#bloodRequestDataTable').dataTable({
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
        "sAjaxSource": "/BloodRequest/GetBloodRequestList",
        "aoColumns": [
            { "sName": "RequesterName" },
            { "sName": "RequesterContactNo" },
            { "sName": "RequesterAmount" },
            { "sName": "PresentLocation" },
            { "sName": "DateOfDonation" },
            { "sName": "RequiredBloodGroup" },
            { "sName": "RequesterStatus" },
            { "sName": "Actions",
                "bSearchable": false,
                "bSortable": false,
                "sWidth": "30px",
                "fnRender": function (oObj) {
                    return '<a class="lnkDetailsBloodRequest  btn btn-success btn-mini" style="margin-right: 5px;" href=\"/BloodRequest/Details/' +
                                oObj.aData[7] + '\" ><icon class="icon-search icon-white"></icon></a>';

                }

            }
            ]
    });

    //end DataTable Script

    //-------------------------------------------------------
    //start Add, Edit, Delete - Dialog, Click Event

        //For details BloodRequest
        $("#detailsBloodRequestDialog").dialog({
            autoOpen: false,
            width: 600,
            resizable: false,
            modal: true,
            buttons: {
                "Cancel": function () {
                    $(this).dialog("close");
                }
            }
        });

        $('#bloodRequestDataTable tbody td a.lnkDetailsBloodRequest').live('click', function () {
            //$('#bloodRequestDataTable tbody td .lnkDetailsBloodRequest').on('click', 'a', function () {

            var linkObj = $(this);
            var dialogDiv = $('#detailsBloodRequestDialog');
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