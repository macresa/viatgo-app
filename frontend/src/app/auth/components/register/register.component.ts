import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Auth } from '../../models/auth';
import { CookieService } from 'ngx-cookie-service';
import { RegisterService } from '../../services/register.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  
  errorFound: boolean = false;
  userName: string = null!;
  loading: boolean = false;

  private cookieService = inject(CookieService);
  private registerService = inject(RegisterService);

  registerForm = new FormGroup (
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
  register(){
    this.clearError();
    this.loader();
   if(this.registerForm.valid){
      const params: Auth = 
      {
        userName: this.registerForm.value.userName!,
        password: this.registerForm.value.password!
      }

    this.registerService.register(params).subscribe({
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
