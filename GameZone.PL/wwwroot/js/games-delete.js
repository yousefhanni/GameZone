//$(document).ready(function () {
//    $('.js-delete').on('click', function () {
//        var btn = $(this);

//        //confirmButton 
//        const swal = Swal.mixin({
//            customClass: {
//                confirmButton: 'btn btn-danger mx-2',
//                cancelButton: 'btn btn-light'
//            },
//            buttonsStyling: false
//        });

//        swal.fire({
//            title: 'Are you sure that you need to delete this game?',
//            text: "You won't be able to revert this!",
//            icon: 'warning',
//            showCancelButton: true,
//            confirmButtonText: 'Yes, delete it!',
//            cancelButtonText: 'No, cancel!',
//            reverseButtons: true
//        }).then((result) => {
//            if (result.isConfirmed) {
//                $.ajax({
//                    url: `/Games/Delete/${btn.data('id')}`,
//                    method: 'DELETE',
//                    success: function () {
//                        swal.fire(
//                            'Deleted!',
//                            'Game has been deleted.',
//                            'success'
//                        );

//                        btn.parents('tr').fadeOut();
//                    },
//                    error: function () {
//                        swal.fire(
//                            'Oooops...',
//                            'Something went wrong.',
//                            'error'
//                        );
//                    }
//                });
//            }
//        });
//    });
//});
$(document).ready(function () {
    $('.js-delete').on('click', function () {
        var btn = $(this);
        var entityId = btn.data('id');
        var entityType = btn.data('type');
        var entityName = btn.data('name') || 'this item';

        // SweetAlert configuration
        const swal = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-danger mx-2',
                cancelButton: 'btn btn-light'
            },
            buttonsStyling: false
        });

        swal.fire({
            title: `Are you sure you want to delete ${entityName}?`,
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'No, cancel!',
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: `/${entityType}/Delete/${entityId}`,
                    method: 'DELETE',
                    success: function () {
                        swal.fire(
                            'Deleted!',
                            `${entityName} has been deleted.`,
                            'success'
                        );

                        btn.parents('tr').fadeOut();
                    },
                    error: function () {
                        swal.fire(
                            'Oooops...',
                            'Something went wrong.',
                            'error'
                        );
                    }
                });
            }
        });
    });
});
