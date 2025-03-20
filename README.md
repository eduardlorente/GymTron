# GymTron

GymTron is a cross-platform mobile application built with.NET MAUI. It follows a Domain-Driven Design (DDD) approach and Clean Architecture principles to ensure a maintainable and scalable codebase. 
This project is created to "play" with dotnet MAUI and try some architectures. 
It's not a project for professional usage.

## Features

*   Cross-platform support (Android done, iOS and others in progress)
*   Intuitive user interface for managing gym-related tasks
*   Secure data storage using MySQL
*   Add workouts, complete exercises, and record details for each exercise.
*   View your workout and exercise history.
*   Track your body measurements (weight and BMI) to monitor your progress.

## Architecture

GymTron is built using the following architectural patterns:

*   Domain-Driven Design (DDD): The application's core logic is organized around the domain model, ensuring that the software reflects the business requirements.
*   Clean Architecture: The codebase is structured in layers, with dependencies pointing inwards, making it highly testable and independent of external frameworks.
*   Repository Pattern: Data access is abstracted through repositories, providing a consistent interface for interacting with the MySQL database.

## Usage

To get started with GymTron, follow these steps:

1.  **Clone the repository:**

2.  **Install dependencies:**

3.  **Set up the database:**
    *   Run the **StartDockerScript.ps1** script to install a MySQL container.
    *   Modify the `appsettings.json` and `appsettings.Development.json` files to point to the correct MySQL connection details.

## Contributing

Contributions to GymTron are welcome! If you'd like to contribute, please follow these guidelines:

1.  Fork the repository.
2.  Create a new branch for your feature or bug fix.
3.  Make your changes and commit them with clear and concise messages.
4.  Submit a pull request.

## License

GymTron is released under the MIT License. This means you are free to use, modify, and distribute the software for both commercial and non-commercial purposes, as long as you include the original copyright notice.

## Contact

If you have any questions or feedback, please feel free to contact me at eduardlorente@gmail.com.

## Acknowledgements

I would like to acknowledge the following resources that helped in the development of GymTron:

*  .NET MAUI documentation
*  Domain-Driven Design principles
*  Clean Architecture guidelines

## TODO/In Progress

*  Configure multi-language support and use resources.
*  Configure ILogging.
*  Configure multi-user support.
*  Many details are still in progress; this is not a finished project.

Thank you for using GymTron!