import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

export const AuthGuard: CanActivateFn = () => 
  {
   const cookieService = inject(CookieService);
   const router = inject(Router);

   const token = cookieService.get('token');

   if(token) {
      return true;
   }

    return router.navigate(['/register']);
}