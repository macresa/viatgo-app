import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { LoggedGuard } from '../guards/logged.guard';
import { AccountComponent } from './components/account/account.component';
import { AuthGuard } from '../guards/auth.guard';

const routes: Routes = [
  {path: 'register', component: RegisterComponent, canActivate: [LoggedGuard]},
  {path: 'login', component: LoginComponent, canActivate: [LoggedGuard]},
  {path: 'account', component: AccountComponent, canActivate: [AuthGuard]},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }
