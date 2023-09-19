window.ShowAlert = (type, message) => {
    if (type == "success") {
        Swal.fire(
            'Good job!',
            message,
            'success'
        )
    }
    else if (type == "error") {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: message,
            footer: '<a href="">Why do I have this issue?</a>'
        })
    }
}