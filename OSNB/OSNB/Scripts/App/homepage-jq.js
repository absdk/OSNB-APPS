

var homePageBloodRequestObjData;

$(function () {
    //start DataTable Script

    homePageBloodRequestObjData = $('#homePageBloodRequestDataTable').dataTable({
        "bJQueryUI": false,
        "bAutoWidth": false,
        //"bLengthChange": false,
        "iDisplayLength": 2,
        "sPaginationType": "bootstrap",
        "bSort": false,
        "oLanguage": {
            //"sLengthMenu": " ",
            //"sLengthMenu": "_MENU_ items per page",
            "sLengthMenu": "<div style='margin-top: 5px !important;'>Current Request List</div>",
            "sZeroRecords": "Data not found",
            "sProcessing": "Loading...",
            "sInfo": "_START_ - _END_ of _TOTAL_ items",
            "sInfoEmpty": "0 - 0 of 0 items",
            "sInfoFiltered": "(filtered from _MAX_ total items)"
        },
        "bProcessing": true,
        "bServerSide": true,
        "sAjaxSource": "/Home/GetBloodRequests",
        "aoColumns": [
            { "sName": "ListView",
                "bSearchable": false,
                "bSortable": false,
                "fnRender": function (oObj) {
                    return '<div class=""><div><strong>Requester Name: </strong><span>' + oObj.aData[0] + '</span></div><div><strong>Required Blood Group: </strong><span>' + oObj.aData[7] + '</span></div><div><strong>Required Blood Amount: </strong><span>' + oObj.aData[2] + '</span></div><div><strong>Present Location: </strong><span>' + oObj.aData[3] + '</span></div><div><strong>Requester Contact No: </strong><span>' + oObj.aData[1] + '</span></div></div>';

                }

            }
            ]
    });

    //end DataTable Script

});