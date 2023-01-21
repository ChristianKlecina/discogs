import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {UserLogin} from "../shared/models/userLogin";
import {MatDialog} from "@angular/material/dialog";
import {Router} from "@angular/router";
import {RegisterUser} from "../shared/models/registerUser";
import {BehaviorSubject, Observable} from "rxjs";
import {IPersistedUser} from "../shared/models/persistedUser";

@Injectable({
  providedIn: 'root'
})
export class LoggingService {

  invalidLogin: boolean
  private currentUserSource = new BehaviorSubject<IPersistedUser>(null);
  currentUser$ = this.currentUserSource.asObservable();





  constructor(private httpClient: HttpClient,private mat: MatDialog, private router: Router) { }

  login(user: UserLogin){


    this.httpClient.post("https://localhost:1296/api/login", user).subscribe((response:IPersistedUser) => {

      this.currentUserSource.next(response)
      //const token = (<any> response).token;
      const token = this.currentUserSource.getValue().token
      localStorage.setItem("jwt",token);
      console.log(localStorage.getItem('jwt'))

      var userjson = window.atob(localStorage.getItem('jwt').split('.')[1])
      //console.log(JSON.parse(this.userjson).role)
      localStorage.setItem("role", JSON.parse(userjson).role)

      this.invalidLogin = false;
      this.mat.closeAll()
      this.router.navigateByUrl('/shop')

      location.reload()

      // console.log(localStorage.getItem('role'))
      // console.log(localStorage.getItem('jwt'))
      // console.log(localStorage.getItem('jwt'))
    }, error => {
      this.invalidLogin = true;

      console.log(error)

    })
  }

  logout(){
    localStorage.removeItem('role');
    localStorage.removeItem('jwt');
    this.router.navigateByUrl('/home')
    location.reload()

    console.log(localStorage.getItem('role'))
  }

  registerUser(user: RegisterUser){
    console.log(user)

    this.httpClient.post("https://localhost:1296/api/user/user", user).subscribe(res => {
      console.log(res)
    }, error => {
      console.log(error)
    })


    this.mat.closeAll()
  }

}
