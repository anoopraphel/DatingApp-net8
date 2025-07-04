import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const routr= inject(Router);
  const toster=inject(ToastrService);
return next(req).pipe(
 catchError(error=>{
  if(error){
    switch(error.status){
      case 400:
        if(error.error.errors){
          const modelStateErrors=[]
          for(const key in error.error.errors){
            if(error.error.errors[key]){
              modelStateErrors.push(error.error.errors[key]);
            }
          }
          throw modelStateErrors.flat()
        }else{
        toster.error(error.error,error.status);
        }
      break;
       case 401:
        toster.error('Unauthorised',error.status);
       break;
       case 404:
        routr.navigateByUrl('/not-found')
       break;
       case 500:
        const navigationExtra:NavigationExtras={state:{error:error.error}};
         routr.navigateByUrl('/server-error',navigationExtra);
       break;
      default:
         toster.error('Something unexpected went wrong',error.status);
          break;
    }
  }
  throw error;
 })
)
};
