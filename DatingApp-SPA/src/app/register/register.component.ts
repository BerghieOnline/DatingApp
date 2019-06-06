import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
// import { userInfo } from 'os';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() CancelRegiser = new EventEmitter();
  model: any = {};

  constructor(private authService: AuthService, private altertify: AlertifyService) { }

  ngOnInit() {
  }

register() {
  this.authService.reggister(this.model).subscribe(() => {
    this.altertify.success('registration successful');
  }, error => {
    this.altertify.error(error);
  });
}

cancel() {
  this.CancelRegiser.emit(false);
}



}
