import { Injectable } from '@angular/core'
import { Store} from '@ngrx/store';
import { TokenService } from "./auth/auth.module";
import { authSignedIn } from "./auth/auth.actions";

@Injectable({providedIn:'root'})
export class AppInitService {

  constructor(private store: Store<{}>,private tokenService:TokenService) {}

  init() {
    if (this.tokenService.tokenExists()) {
      const token = this.tokenService.getToken();
      const parsedUser = this.tokenService.toUser(token);
      this.store.dispatch(authSignedIn({ token: token, user: parsedUser}));
    }
  }
}
