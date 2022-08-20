import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { catchError, Observable, retry, throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth/auth.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private toastr: ToastrService, private authService: AuthService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request)
      .pipe(
        retry(1),
        catchError((err) => {
          if (err.status === 401) {
            err.errorMessage = 'Трябва да влезете с акаунта си!';
            this.authService.logOut();
          } else if (err.status === 404) {
            err.errorMessage = 'Този ресурс не е намерен';
          }

          this.toastr.error(err.errorMessage);

          return throwError(() => err);
        })
      );
  }
}
