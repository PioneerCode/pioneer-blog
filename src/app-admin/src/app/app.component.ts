import { Component } from '@angular/core';

import { AuthenticationService } from './services/authentication.service';

@Component({
  selector: 'pc-app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'app';

  constructor(public authService: AuthenticationService) {
  }
}
