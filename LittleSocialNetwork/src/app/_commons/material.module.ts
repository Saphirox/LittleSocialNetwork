import { MatButtonModule, MatFormFieldControl, MatInputModule, MatFormFieldModule, MatCardModule, MatCardContent } from '@angular/material';
import { NgModule } from '@angular/core';
import {MatGridListModule} from '@angular/material/grid-list';

@NgModule({
    imports: [
        MatButtonModule,
        MatInputModule,
        MatFormFieldModule,
        MatCardModule,
        MatGridListModule
    ],
    exports: [
        MatButtonModule,
        MatInputModule,
        MatFormFieldModule,
        MatCardModule,
        MatGridListModule
    ]
})
export class MaterialModule {
}
