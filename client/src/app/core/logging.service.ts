import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {UserLogin} from "../shared/models/userLogin";
import {MatDialog} from "@angular/material/dialog";
import {Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class LoggingService {

  invalidLogin: boolean




  constructor(private httpClient: HttpClient,private mat: MatDialog, private router: Router) { }

  login(user: UserLogin){


    this.httpClient.post("https://localhost:1296/api/login", user).subscribe(response => {
      const token = (<any> response).value;

      localStorage.setItem("jwt",token);
      console.log(localStorage.getItem('jwt'))

      var userjson = window.atob(localStorage.getItem('jwt').split('.')[1])
      //console.log(JSON.parse(this.userjson).role)
      localStorage.setItem("role", JSON.parse(userjson).role)

      this.invalidLogin = false;
      this.mat.closeAll()
      this.router.navigateByUrl('/shop')

      location.reload()
      console.log(localStorage.getItem('role'))
    }, error => {
      this.invalidLogin = true;
      console.log(error)

    })
  }

  logout(){
    localStorage.removeItem('role');
    this.router.navigateByUrl('/home')
    //location.reload()

    console.log(localStorage.getItem('role'))
  }

}
