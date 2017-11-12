import { MatButtonModule, MatFormFieldControl, MatInputModule, MatFormFieldModule, MatCardModule, MatCardContent } from '@angular/material';
import { NgModule } from '@angular/core';


@NgModule({
    imports: [
        MatButtonModule,
        MatInputModule,
        MatFormFieldModule,
        MatCardModule
    ],
    exports: [
        MatButtonModule,
        MatInputModule,
        MatFormFieldModule,
        MatCardModule
    ]
})
export class MaterialModule {}