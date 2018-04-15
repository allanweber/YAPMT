declare var swal: any;

export class MessageService {
  static fatalError(message: string) {
    swal({
      type: 'error',
      title: 'Fatal Error',
      text: message,
    });
  }

  static SuccessToaster(message: string) {
    swal({
      type: 'success',
      text: message,
      toast: true,
      timer: 3000,
      showConfirmButton: false,
      position: 'top-end',
      background: '#c7e2d5',
    });
  }

  static ErrorToaster(message: string) {
    swal({
      type: 'error',
      text: message,
      toast: true,
      timer: 3000,
      showConfirmButton: false,
      position: 'top-end',
      background: '#e2c7d4',
    });
  }

  static Confirm(message: string): Promise<boolean> {
    return swal({
      type: 'question',
      text: message,

      showCancelButton: true,
      confirmButtonText: 'Yes!',
      cancelButtonText: 'No!',
      confirmButtonClass: 'btn btn-success margin-confirm',
      cancelButtonClass: 'btn btn-danger margin-confirm',
      buttonsStyling: false,
      reverseButtons: true,
    }).then(result => {
      if (result.value) {
        return true;
      } else {
        return false;
      }
    });
  }
}
