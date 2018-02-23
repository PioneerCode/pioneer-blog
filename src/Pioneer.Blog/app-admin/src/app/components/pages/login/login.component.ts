import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../../../services/authentication.service';
import { UserRepository, ILoginRequest } from '../../../repositories/user.repository';
import { IToken } from '../../../models/user';

@Component({
  selector: 'pc-app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginRequest = {} as ILoginRequest;
  loading = false;
  returnUrl: string;

  constructor(
    private userRepository: UserRepository,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService
  ) { }


  ngOnInit() {
    // reset login status
    this.authenticationService.logout();

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/posts';
  }

  onSubmitForm() {
    this.loading = true;
    this.userRepository.login({ username: this.loginRequest.username, password: this.loginRequest.password } as ILoginRequest)
      .subscribe((token: IToken) => {
        console.log(token);
        if (token && token.token) {
          this.authenticationService.setCurrentToken(token);
        }
        this.router.navigate([this.returnUrl]);
        this.loading = false;
      }, error => {
        //  this.alertService.error(error);
        console.log(error);
        this.loading = false;
      });
  }
}
