import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { userInfo } from 'os';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() CancelRegiser = new EventEmitter();
  model: any = {};

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

register() {
  this.authService.reggister(this.model).subscribe(() => {
    console.log('registration successful');
  }, error => {
    console.log(error);
  });
}

cancel() {
  this.CancelRegiser.emit(false);
  console.log('cancelled');
}



}
