export default  interface Category{
    id: string;
    name: string;
}

export class CategoryValues implements Category{
    id!: string;
    name!: string;

    constructor(category?: Category) {
        if (category) {
          this.id = category.id;
          this.name = category.name
        }
    }

}