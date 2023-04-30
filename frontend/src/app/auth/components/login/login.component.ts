import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LoginService } from '../../services/login.service';
import { Router } from '@angular/router';
import { Auth } from '../../models/auth';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  errorFound: boolean = false;
  userName: string = null!;
  loading: boolean = false;

  private cookieService = inject(CookieService);
  private loginService = inject(LoginService);

  loginForm = new FormGroup (
  {
    'userName': new FormControl('', Validators.required),
    'password': new FormControl('', [Validators.required, Validators.minLength(6)])
  });

  constructor(private router: Router){}

  clearError(){
    this.errorFound = false;
  }
  loader(){
    this.loading = true;
  }
  login(){
    this.clearError();
    this.loader();
   if(this.loginForm.valid){   
   const params: Auth = 
   {
     userName: this.loginForm.value.userName!,
     password: this.loginForm.value.password!
   }
  
    this.loginService.login(params)
      .subscribe({
      next: (v) => 
      {
        this.userName = v.userName,
        this.cookieService.set('token', v.token!),
        setTimeout(() => 
        {
          this.loading = false;
          this.router.navigate(['/account']);
        }, 2000);
      },
      error: (e) => 
      {
        setTimeout(() => 
        {
          this.loading = false;
          this.errorFound = true    
        }, 2000);
      }
    });
   }
  }
}
