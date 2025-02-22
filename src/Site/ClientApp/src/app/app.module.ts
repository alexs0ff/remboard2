import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';

import { UiCommonModule } from './ui-common/ui-common.module';
import { ComponentSidenavModule } from "./menu/component-sidenav/component-sidenav";
import { StoreModule } from '@ngrx/store';
import { reducers, metaReducers } from './reducers';
import { MenuModule } from "./menu/menu.module";
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { environment } from '../environments/environment';
import { EffectsModule } from '@ngrx/effects';
import { MenuEffects } from "./menu/menu.effects";
import { AuthModule, AuthEffects } from './auth/auth.module';
import { httpInterceptorProviders } from "./http/interceptors";
import { MessageFlowModule } from "./message-flow/message-flow.module";
import { RaCrudsModule, CrudsEntityMetadata, EntitySchemaMetadata, EntityEditSchemaMetadata } from "./features/ra-cruds/ra-cruds.module";


const configCrudsEntityMetadata: CrudsEntityMetadata = {};
const configEntitySchemaMetadata: EntitySchemaMetadata = {};
const configEntityEditSchemaMetadata: EntityEditSchemaMetadata = {};

RaCrudsModule.prepareReducersMap(configCrudsEntityMetadata, configEntitySchemaMetadata, configEntityEditSchemaMetadata,reducers);

@NgModule({
	declarations: [
		AppComponent,
		NavMenuComponent,
		HomeComponent,
		CounterComponent,
		FetchDataComponent
	],
	imports: [
		BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
		HttpClientModule,
		RouterModule.forRoot([
			{ path: '', component: HomeComponent, pathMatch: 'full' },
			{ path: 'counter', component: CounterComponent },
			{ path: 'fetch-data', component: FetchDataComponent },
			{ path: 'orders', loadChildren: () => import('./features/orders/orders.module').then(m => m.OrdersModule) },
			{ path: 'users', loadChildren: () => import('./features/users/users.module').then(m => m.UsersModule) },
		]),
		UiCommonModule,
		MenuModule,
		StoreModule.forRoot(reducers,
			{
				metaReducers,
				runtimeChecks: {
					strictStateImmutability: true,
					strictActionImmutability: true
				}
			}),
		StoreDevtoolsModule.instrument({ maxAge: 25, logOnly: environment.production }),
		EffectsModule.forRoot([MenuEffects, AuthEffects]),
		AuthModule,
		MessageFlowModule,
		RaCrudsModule.forRoot()
	],
	providers: [httpInterceptorProviders],
	bootstrap: [AppComponent]
})
export class AppModule {
}
