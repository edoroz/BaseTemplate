
$(document).ready(function () {
    GetUsers();
});

function GetUsers() {
   
    fetch('/api/UserApi/GetUsers')
        .then(r => r.json())
        .then(result => {
            if ($.fn.DataTable.isDataTable('#tblUser')) {
                $('#tblUser').DataTable().destroy();
            }
            $('#tblUser').DataTable({
                responsive: true,
                scrollX: true, lengthChange: false,
                layout: { topStart: 'search', top: 'pageLength', topEnd: 'info', bottom: 'paging', bottomStart: 'paging', bottomEnd:'paging' },
                data: result.data,
                columnDefs: [
                    { targets: 0, className: 'dt-center' },
                    { targets: 1, className: 'dt-head-center' },
                    { targets: -1, className: 'text-center'}
                ],
                columns: [
                    {
                        data: null,
                        render: function (data, type, row) {
                            const rowData = encodeURIComponent(JSON.stringify(row));
                            return `
                        <div class="btn-group">
                            <a onclick="openModal('${rowData}')"
                               class="btn btn-outline-primary">
                                <i class="far fa-edit"></i>
                            </a>&nbsp;
                            <a onclick="removeCompliance('${row.Id}')"
                               class="btn btn-outline-danger">
                                <i class="fa fa-trash"></i>
                            </a>
                        </div>`;
                        }
                    },
                    {  data: 'userName' },
                    {  data: 'fullName' },
                    {  data: 'email' },
                    {  data: 'phoneNumber' },
                    
                ]
            });

        })
        .catch(err => {
            console.error(err);
            toastr.error("Error loading data");
        });
}