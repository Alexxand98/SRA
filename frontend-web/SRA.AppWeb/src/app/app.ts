import { importProvidersFrom } from '@angular/core';
import { provideHttpClient } from '@angular/common/http';
import { bootstrapApplication } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { appConfig } from './app.config';
import { AuthModule } from './auth/auth.module';

bootstrapApplication(AppComponent, {
  ...appConfig,
  providers: [
    importProvidersFrom(AuthModule),
    provideHttpClient(),
    ...appConfig.providers!
  ]
});
