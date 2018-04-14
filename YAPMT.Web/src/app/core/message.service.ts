declare var swal: any;

export class MessageService {
  static fatalError(message: string) {
    swal({
      type: 'error',
      title: 'Ocorreu um erro',
      text: message
    });
  }

  static SuccessToaster(message: string) {
    swal({
      type: 'success',
      text: message,
      toast: true,
      timer: 3000,
      showConfirmButton: false,
      position: 'top-end'
    });
  }

  static ErrorToaster(message: string) {
    swal({
      type: 'error',
      text: message,
      toast: true,
      timer: 3000,
      showConfirmButton: false,
      position: 'top-end'
    });
  }

  static Confirm(message: string): Promise<boolean> {
    return swal({
      type: 'question',
      text: message,

      showCancelButton: true,
      confirmButtonText: 'Sim, remover!',
      cancelButtonText: 'Não, cancelar!',
      confirmButtonClass: 'btn btn-success margin-confirm',
      cancelButtonClass: 'btn btn-danger margin-confirm',
      buttonsStyling: false,
      reverseButtons: true
    }).then(result => {
      if (result.value) {
        return true;
      } else {
        return false;
      }
    });
  }
}
