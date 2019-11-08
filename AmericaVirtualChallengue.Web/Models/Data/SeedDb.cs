namespace AmericaVirtualChallengue.Web.Models.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AmericaVirtualChallengue.Web.Helpers;
    using Entities;
    using Microsoft.AspNetCore.Identity;

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private Random random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            // Check roles
            await this.CheckRoles();

            await this.CheckUserAsync("virtual.america@gmail.com", "Virtual", "America", "User");
            await this.CheckUserAsync("homero.simpson@gmail.com", "Homero", "Simpson", "User");
            await this.CheckUserAsync("sevann.radhak@gmail.com", "Sevann", "Radhak", "Admin");

            // Add products
            if (!this.context.Products.Any())
            {
                // Add topics
                Topic firstTopic = new Topic { Description = "Requirements analysis" };
                Topic secondTopic = new Topic { Description = "Desktop and mobile interfaces design" };
                Topic thirdtTopic = new Topic { Description = "Frontend and backend development" };
                this.AddTopic(firstTopic);
                this.AddTopic(secondTopic);
                this.AddTopic(thirdtTopic);

                Product product = this.AddProduct(
                    "Usability and Government 2.0",
                    "When possible, include citizens in the planning stages The biggest challenges to technology projects are the assumptions " +
                    "and questions that go unanswered.Agencies have an opportunity to include citizens informally, asking those they have interactions " + 
                    "with on social networks or call centers for feedback on ideas.They also may create longer - term citizen advisory committees, similar.",
                    "one");
                this.AddProductsTopics(product, firstTopic);
                this.AddProductsTopics(product, secondTopic);
                this.AddProductsTopics(product, thirdtTopic);

                product = this.AddProduct(
                    "Design Space",
                    "Designing with technical objectives and options in the context of design projects, technology can be seen as any tool, technique, or " +
                    "machine that supports a task relevant to the design challenge at hand.Models centered on technology are driven by considerations of " +
                    "feasibility and technical possibility.They have the goal of mapping the space of technical options, asserting the limitations and possibilities.",
                    "two");
                this.AddProductsTopics(product, firstTopic);
                this.AddProductsTopics(product, thirdtTopic);

                this.AddProduct(
                    "Master Data and Master Data Management",
                    "Although MDM should not be considered a technology project, it is clear that developing an MDM program cannot be done without leveraging tools and technology. " +
                    "The technical aspects of master data management rely on tools supporting data profiling, complex data analysis, metadata management, data modeling, " +
                    "data integration, parsing, standardization, record linkage and matching, cleansing, services-oriented architecture, access control, data federation.",
                    "three");
                this.AddProductsTopics(product, secondTopic);
                this.AddProductsTopics(product, thirdtTopic);

                product = this.AddProduct(
                    "Putting it together",
                    "The locus of decision-making – business ownership. For many reasons, business involvement in technology projects is a critical success factor. " + 
                    "This will be no exception in Web 2.0 projects – indeed it may be even more pronounced.The McKinsey July 2008 Report on Web 2.0 concludes with a " + 
                    "critical and unambiguous indicator of Web 2.0 satisfaction: those companies with the lowest levels of satisfaction with Web 2.0 technologies.",
                    "four");
                this.AddProductsTopics(product, firstTopic);

                product = this.AddProduct(
                    "The Evolution of Cybercrime",
                    "Dr. J.C.R. Licklider was appointed to run ARPA's computer technology project in 1962. He was largely responsible for building the beginnings of a WAN " + 
                    "connecting government/military and university sites, using redundant links so that if one node was taken out, messages could still get through by taking a " + 
                    "different path. This network, based on packet-switching technology developed in the 1960s, was called the ARPANET.",
                    "five");

                product = this.AddProduct(
                    "Development of Our Country's IT Industry in the New Period*",
                    "There are already success stories of relying on major national projects and special science and technology projects to achieve core technological " + 
                    "breakthroughs in some industries. To implement projects, we should focus on key development areas of the IT industry, make the best use of limited " +
                    "resources and motivate everyone involved in order to tackle difficult technological problems and translate research results into actual productive forces.",
                    "six");
                this.AddProductsTopics(product, firstTopic);

                product = this.AddProduct(
                    "Patchwork redux: how today's systems librarians enrich the weave of library culture",
                    "Whether out of necessity or serendipitously, systems librarians and systems staff have become accustomed to the teamwork approach. When a technology project " +
                    "needs to be completed, systems librarians coordinate the necessary experts required to get the tasks done, whether they are the city's IT department, the school's "+
                    "technology consultant, the college's IT staff, other library staff, technology vendors or training providers. The systems librarian involves anyone with knowledge.",
                    "seven");
                this.AddProductsTopics(product, firstTopic);

                product = this.AddProduct(
                    "Road Map",
                    "Applications Road Map—the identification and sequencing of the business, technology, and support projects required to manage and deliver data and content. Once " +
                    "EIM is established or has been attempted, the Road Map requires revisiting. When revisited, this activity focuses on resequencing or adjusting projects. " +
                    "Revisiting this activity can happen at any time, but at minimum I recommend this activity be revisited annually, given changes in the business environment." +
                    "or organization priorities.",
                    "eigth");
                this.AddProductsTopics(product, firstTopic);
                
                this.AddProduct(
                    "Types of Certification and Accreditation",
                    "DCID 6/3 is the certification and accreditation process used by federal agencies working on intelligence projects (e.g., the CIA). Specifically, information " +
                    "technology projects that require that anyone working on them has a Top Secret, Sensitive Compartmentalized Information (SCI) clearance use the DCID 6/3 process.",
                    "nine");

                this.AddProduct(
                    "The Enterprise Data Warehouse",
                    "Although the DCID 6/3 model was designed for classified information and intelligence work, it is publicly available for review, and any agency or private " +
                    "organization can adopt the methodology, and customize it according to their own unique requirements. The DCID Standards Manual, which defines the DCID 6/3 " +
                    "certification",
                    "ten");

                this.AddProduct(
                    "Automatic Laser Level Made From an Old Hard ",
                    "Often forgotten or neglected in the enterprise context, this involves working with digital media and front-end technology to convey information and make interaction " +
                    "accessible to people. As is the last mile of technology, this involves specifying the channel used such as web or print, as well as technical components like pieces." +
                    "software, sensors, devices, or projections.",
                    "eleven");

                this.AddProduct(
                    "The Feel Like a Queen Coffee Machine",
                    "Because of the size and diversity of the field of engineering, there is an overwhelming number of disciplines that are relevant to Technology Design. Although technology plays " +
                    "a significant role in nearly all design disciplines.",
                    "twelve");

                this.AddProduct(
                    "Automatic Laser Level Made From an Old Hard ",
                    "We need to combine science and technology closely with the economy, have the state play an important role in organizing, coordinating and guiding industrial development, " +
                    "formulate plans, marshal resources",
                    "thirteen");

                this.AddProduct(
                    "A Fully Automatic Coffee Bean Roaster",
                    "To implement projects, we should focus on key development areas of the IT industry, make the best use of limited resources and motivate everyone involved in order to tackle difficult " +
                    "technological problems and translate research results into actual productive forces.",
                    "fourteen");

                this.AddProduct(
                    "Reverse Engineering: USB Controlled Home ",
                    "What platforms, devices, machines, networks, and data stores are being used? The bottom layer describes the available infrastructure and resources and how they are used by " +
                    "the system to perform its functions. Decisions made about this layer relate to concerns of performance, security, and availability.",
                    "fifteen");

                this.AddProduct(
                    "Turn on and Off Your Things With a Laser!",
                    "WAN connecting government/military and university sites, using redundant links so that if one node was taken out, messages could still get through by taking a different path. " +
                    "This network, based on packet-switching technology developed in the 1960s, was called the ARPANET.",
                    "sixteen");

                this.AddProduct(
                    "Control Your TV With Your Phone",
                    "While business involvement in IT projects is now received wisdom, this observation perhaps raises the stakes: lack of involvement by the IT department as a " +
                    "critical success factor! In all likelihood this reflects a strong readiness, leadership and feeling of perceived control over the technology in user departments.",
                    "seventeen");

                this.AddProduct(
                    "Wireless Multi-Channel Voice-Controlled Electrical ",
                    "often forgotten or neglected in the enterprise context, this involves working with digital media and front-end technology to convey information and make interaction " +
                    "accessible to people. As is the last mile of technology, this involves specifying the channel used such as web or print, as well as technical components like pieces of " +
                    "software, sensors, devices, or projections..",
                    "eigthteen");

                this.AddProduct(
                    "Wireless Christmas Light Timer With Raspberry",
                    "The structure of the domain, the objects and their relationships, form the basis for modeling technical components, infrastructure, platforms, and representation in data models " +
                    "and code. The other way around, technical components will be objects introduced into that structure, visible to users as tools or devices.",
                    "nineteen");

                this.AddProduct(
                    "Family Activity Portraits",
                    "Modeling technology usually describes system architectures of interrelated components on multiple layers or tiers, capturing their dependencies " +
                    "and interfaces in structural or procedural diagrams and separating different concerns. A typical layered architecture consists of about four layers, " +
                    "but there are model variants combining or further dividing these layers, and much more complex models",
                    "twenty");

                this.AddProduct(
                    "Twitter Controlled Pet Feeder",
                    "IT department (36 per cent of those dissatisfied compared with 11 per cent of those satisfied). Conversely, 25 per cent of those satisfied with " +
                    "Web 2.0 do this in a context where ‘the business identified new technologies and brings them into the company without IT support’",
                    "twentyone");

                await this.context.SaveChangesAsync();
            }

        }

        private async Task CheckRoles()
        {
            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("User");
        }

        private async Task<User> CheckUserAsync(string userName, string firstName, string lastName, string role)
        {
            // Add user
            var user = await this.userHelper.FindByEmailAsync(userName);
            if (user == null)
            {
                user = await this.AddUser(userName, firstName, lastName, role);
            }

            var isInRole = await this.userHelper.IsUserInRoleAsync(user, role);
            if (!isInRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, role);
            }

            return user;
        }

        private async Task<User> AddUser(string userName, string firstName, string lastName, string role)
        {
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = userName,
                UserName = userName,
                PhoneNumber = "1173627795"
            };

            var result = await this.userHelper.CreateAsync(user, userName);
            if (result != IdentityResult.Success)
            {
                throw new InvalidOperationException("Could not create the user in seeder");
            }

            await this.userHelper.AddUserToRoleAsync(user, role);

            if(user.UserName == "sevann.radhak@gmail.com")
            {
                await this.userHelper.AddUserToRoleAsync(user, "User");
            }
            //var token = await this.userHelper.GenerateEmailConfirmationTokenAsync(user);
            //await this.userHelper.ConfirmEmailAsync(user, token);
            return user;
        }

        private Product AddProduct(string name, string description, string imageUrl)
        {
            var product = new Product
            {
                Name = name,
                Price = random.Next(1000, 100000),
                Description = description,
                IsAvailabe = true,
                ImageUrl = $"~/images/Products/{imageUrl}.jpg"
            };

            this.context.Products.Add(product);

            return product;
        }

        private void AddProductsTopics(Product product, Topic topic)
        {
            this.context.ProductTopics.Add(new ProductsTopic
            {
                Product = product,
                Topic = topic
            });
        }

        private void AddTopic(Topic topic)
        {
            this.context.Topics.Add(topic);
        }
    }

}
