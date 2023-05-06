import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  errorMessage: string = "";

  form: FormGroup = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
  });

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {

  }

  onSubmit() {
    this.authService.login(this.form.value)
      .subscribe(
        next => { this.errorMessage = ""; },
        err => { 
          console.log(err);
          this.errorMessage = "Username or password is wrong."; 
        },
        () => { this.router.navigate(['/home']); }
      );
  }


}
