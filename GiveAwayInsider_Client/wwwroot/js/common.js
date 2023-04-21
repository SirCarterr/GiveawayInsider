
window.ShowSwalToast = (message) => {
    Swal.fire({
        toast: true,
        position: 'bottom-end',
        icon: 'success',
        title: message,
        showConfirmButton: false,
        timer: 2000,
        timerProgressBar: true
    })
}

window.ShowSwalToast2 = (message) => {
    Swal.fire({
        toast: true,
        position: 'bottom-end',
        icon: 'info',
        title: message,
        showConfirmButton: false,
        timer: 2000,
        timerProgressBar: true
    })
}

window.ShowSwalToast3 = (message) => {
    Swal.fire({
        toast: true,
        position: 'bottom-end',
        icon: 'error',
        title: message,
        showConfirmButton: false,
        timer: 2000,
        timerProgressBar: true
    })
}

window.ShowSwalInfo = (message) => {
    Swal.fire({
        title: 'Search instruction',
        html: message,
        icon: 'info'
    })
}