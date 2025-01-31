using WebAppAss.Data;
using WebAppAss.Models;

namespace WebAppAss.Data
{
    public static class DBInitializer
    {
        public static void Initialize(WebAppAssContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Seed Drinks
            if (!context.Drinks.Any())
            {
                var drinks = new List<Drink>
            {
                new Drink
                {
                    Name = "Classic Lemonade",
                    Description = "A refreshing homemade lemonade made from freshly squeezed lemons and a touch of sugar.",
                    Price = 4.50m,
                    Size = "500ml",
                    IsAlcoholic = false,
                    IsAvailable = true
                },
                new Drink
                {
                    Name = "Iced Tea",
                    Description = "Our signature iced tea, brewed fresh daily and served over ice with a slice of lemon.",
                    Price = 3.00m,
                    Size = "375ml",
                    IsAlcoholic = false,
                    IsAvailable = true
                },
                new Drink
                {
                    Name = "Root Beer Float",
                    Description = "A creamy delight made with our rich vanilla ice cream and topped with a generous pour of root beer.",
                    Price = 5.50m,
                    Size = "500ml",
                    IsAlcoholic = false,
                    IsAvailable = true
                },
                new Drink
                {
                    Name = "Sam&Ella's Craft Lager",
                    Description = "A locally brewed lager that pairs perfectly with any of our burgers. Crisp and refreshing.",
                    Price = 6.50m,
                    Size = "Pint",
                    IsAlcoholic = true,
                    IsAvailable = true
                },
                new Drink
                {
                    Name = "Virgin Mojito",
                    Description = "A non-alcoholic version of our popular mojito, made with fresh mint leaves, lime juice, and soda water.",
                    Price = 3.50m,
                    Size = "375ml",
                    IsAlcoholic = false,
                    IsAvailable = true
                }
            };

                context.Drinks.AddRange(drinks);
                context.SaveChanges();
            }

            // Seed Desserts
            if (!context.Desserts.Any())
            {
                var desserts = new List<Dessert>
            {
                new Dessert
                {
                    Name = "Chocolate Lava Cake",
                    Description = "A rich chocolate cake with a gooey molten chocolate center, served warm with a scoop of vanilla ice cream.",
                    Price = 7.99m,
                    IsWarmDessert = true,
                    IsAvailable = true
                },
                new Dessert
                {
                    Name = "New York Cheesecake",
                    Description = "A classic creamy cheesecake with a graham cracker crust, topped with fresh strawberries.",
                    Price = 7.50m,
                    IsWarmDessert = false,
                    IsAvailable = true
                },
                new Dessert
                {
                    Name = "Banana Split",
                    Description = "Three scoops of ice cream (chocolate, vanilla, and strawberry) topped with bananas, whipped cream, nuts, and cherries.",
                    Price = 5.99m,
                    IsWarmDessert = false,
                    IsAvailable = true
                },
                new Dessert
                {
                    Name = "Warm Brownie Sundae",
                    Description = "A freshly baked fudge brownie, served warm and topped with hot fudge, whipped cream, and two scoops of ice cream.",
                    Price = 6.99m,
                    IsWarmDessert = true,
                    IsAvailable = true
                }
            };

                context.Desserts.AddRange(desserts);
                context.SaveChanges();
            }

            // Seed Sides
            if (!context.Sides.Any())
            {
                var sides = new List<Side>
            {
                new Side
                {
                    Name = "French Fries",
                    Description = "Crispy golden fries served with your choice of dipping sauce.",
                    Price = 3.99m,
                    Size = "Medium",
                    IsAvailable = true
                },
                new Side
                {
                    Name = "Onion Rings",
                    Description = "Thick-cut onion rings, battered and fried to perfection. Served with a tangy dipping sauce.",
                    Price = 4.50m,
                    Size = "Large",
                    IsAvailable = true
                },
                new Side
                {
                    Name = "Sweet Potato Fries",
                    Description = "Seasoned sweet potato fries, crispy on the outside and tender on the inside. Served with a honey mustard dip.",
                    Price = 3.50m,
                    Size = "Medium",
                    IsAvailable = true
                },
                new Side
                {
                    Name = "Coleslaw",
                    Description = "Freshly made coleslaw with crisp cabbage, carrots, and a creamy dressing. Perfectly pairs with any burger or sandwich.",
                    Price = 2.9m,
                    Size = "Small",
                    IsAvailable = true
                }
            };

                context.Sides.AddRange(sides);
                context.SaveChanges();
            }

            // Seed Burgers
            if (!context.Burgers.Any())
            {
                var burgers = new List<Burger>
            {
                new Burger
                {
                    Name = "Classic Beef Burger",
                    Description = "A juicy beef patty topped with cheddar cheese, lettuce, tomato, onion, pickles, and our special sauce on a toasted bun.",
                    Price = 10.99m,
                    Type = "Beef",
                    IsVegetarian = false,
                    IsAvailable = true,
                    Special = false
                },
                new Burger
                {
                    Name = "BBQ Bacon Burger",
                    Description = "Smoky beef patty topped with crispy bacon, cheddar cheese, and drizzled with our homemade BBQ sauce. Served on a sesame seed bun.",
                    Price = 11.99m,
                    Type = "Beef",
                    IsVegetarian = false,
                    IsAvailable = true,
                    Special = false
                },
                new Burger
                {
                    Name = "Spicy Chicken Burger",
                    Description = "Crispy fried chicken patty seasoned with our signature spices, topped with pepper jack cheese, jalapeños, and chipotle mayo on a brioche bun.",
                    Price = 9.99m,
                    Type = "Chicken",
                    IsVegetarian = false,
                    IsAvailable = true,
                    Special = false
                },
                new Burger
                {
                    Name = "Veggie Burger",
                    Description = "A hearty veggie patty made from black beans, quinoa, and roasted vegetables, topped with lettuce, tomato, and avocado on a multigrain bun.",
                    Price = 9.50m,
                    Type = "Beef", // Technically a vegetarian option but categorized under "Beef" for simplicity
                    IsVegetarian = true,
                    IsAvailable = true,
                    Special = false
                },
                new Burger
                {
                    Name = "Mushroom Swiss Burger",
                    Description = "Juicy beef patty topped with sautéed mushrooms, Swiss cheese, and garlic aioli on a toasted bun.",
                    Price = 10.50m,
                    Type = "Beef",
                    IsVegetarian = false,
                    IsAvailable = true,
                    Special = true
                },
                new Burger
                {
                    Name = "Buffalo Chicken Burger",
                    Description = "Spicy fried chicken patty smothered in buffalo sauce, topped with blue cheese crumbles and celery sticks on a brioche bun.",
                    Price = 11.99m,
                    Type = "Chicken",
                    IsVegetarian = false,
                    IsAvailable = true,
                    Special = true
                }
            };

                context.Burgers.AddRange(burgers);
                context.SaveChanges();
            }
        }
    }
}