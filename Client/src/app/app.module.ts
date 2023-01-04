import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AdminModule } from './admin/admin.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PublicModule } from './public/public.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtHttpInterceptor } from 'src/helpers/interceptors/jwtHttpInterceptor';
import { UserModule } from './user/user.module';
import { AuthenticationModule } from './authentication/authentication.module';
import { FoodModule } from './food/food.module';

@NgModule({
  declarations: [
    AppComponent,
    ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    AdminModule,
    UserModule,
    AuthenticationModule,
    PublicModule,
    FoodModule,
  ],
  providers: [
    { 
      provide: HTTP_INTERCEPTORS, useClass: JwtHttpInterceptor, multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
