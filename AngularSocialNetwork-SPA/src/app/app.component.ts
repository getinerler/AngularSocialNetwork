import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './_services/auth.service';
import { faHome, faUser, faBell, faRightFromBracket } from '@fortawesome/free-solid-svg-icons';
import { User } from './_models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {

  id: number = -1;
  faHome = faHome;
  faUser = faUser;
  faBell = faBell;
  faRightFromBracket = faRightFromBracket;

  constructor( private router: Router, private authService: AuthService) { 

  }

  ngOnInit(){
    let user: User = JSON.parse(localStorage.getItem("user")?.toString() ?? "{}");
    this.id = user.id;
  }

  showNav() {
    return location.pathname.indexOf('/login') === -1;
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token;
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decodedToken = null;
    this.authService.currentUser = null!;
    this.router.navigate(['/login']);
  }
}