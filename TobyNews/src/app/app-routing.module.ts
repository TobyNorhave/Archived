import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./tabs/tabs.module').then(m => m.TabsPageModule)
  },
  // { path: 'source', loadChildren: './source/source.module#SourcePageModule' }
  // { path: 'top-news', loadChildren: './top-news/top-news.module#TopNewsPageModule' },
  // { path: 'headlines', loadChildren: './headlines/headlines.module#HeadlinesPageModule' },
  // { path: 'sources', loadChildren: './sources/sources.module#SourcesPageModule' },
  // { path: 'favorites', loadChildren: './favorites/favorites.module#FavoritesPageModule' },
  // { path: 'settings', loadChildren: './settings/settings.module#SettingsPageModule' }
];
@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
