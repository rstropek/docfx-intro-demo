# .NET Documentation using DOCFX

## Introduction

This tutorial helps you create doucmentation for your ASP.NET core-project using DOCFX. Docfx's features are generating documentation from your C#-code, as well as from Swagger-conform json-files describing a REST API. You can also include conceptual markdown files to have documentation written in your own words.

## Prerequisites

Make sure you have a recent edition of Visual Studio as well as ASP.NET Core installed.

## Getting Started

> If you do not want to create the demo solution manually, you can copy the [*src* folder from GitHub](https://github.com/rstropek/docfx-intro-demo/tree/master/src).

First of all we set up 3 simple .NET Core projects to generate documentation from. In your project's root folder, create a *src* folder. Put the following three projects into this *src* folder.

Open Visual Studio and create a new .NET Core-Web application and select API. Next create a new .NET core-class library within your solution and add the following code. This is the small business logic in our example.

[!code-csharp[SimpleDivision](../../../src/CalculatorLibrary/Calculator.cs)]

In the Web Application replace the Controller's code with the following one. The Web API Controller is just responsible for calling a method that's in our business logic and return the result. Nothing special.

[!code-csharp[CalculatorController](../../../src/Calculator/Controllers/CalculatorController.cs)]

Create a model folder within the WebApi project and the following .cs file:

[!code-csharp[CalculationParams](../../../src/Calculator/Model/CalculationParams.cs)]

Next we create a .NET Core xUnit Test project with a simple unit test as follows:

[!code-csharp[SimpleDivision](../../../src/CalculatorTest/CalculatorTest.cs)]

Now the code is set for our little example.

## Create Empty Web Project for Documentation

Next step is setting up the documentation. For that create a *docs* folder in the root folder of your project. In *docs*, create a new empty .NET Core Web project and install the NuGet-package [*docfx.console*](https://www.nuget.org/packages/docfx.console/).

## Generating code documentation

Building the docs web project will set up all files needed for the documentation.

The documentation output is saved to a _site directory within the project root, and can be served by any web server.

For our code to be found we have to tell docfx where it is, that's done within `metadata` in our *docfx.json*; specify the path to your src:

[!code-json[docfx.json](../docfx.json#L4-L11)]

### The toc-files

The toc-files (table of content) are responsíble for the docs site's structure.

*yml* and *md* can be used as file types, although *yml* is recommended, as it is capable of more functionality.

A sample toc-file could look like this:

[!code-yaml[toc.yml](../toc.yml)]

Each bullet point is one element in the site's nav bar at the top.
*name* is the title displayed in the documentation and *href* is the relative path to a folder.

Within the folders there can be *yml* files describing code or *md* files with text or another toc file. The second toc file's content is displayed in a nav bar at the left-hand side of the page. But you can also nest toc-files even more - DocFx searches the folder structure recursively for toc-files. Those other toc-files will be nested on the left nav bar.

## Getting documentation for REST Services

DocFx is able to read REST service information that's in swagger/open api format.
Just put those file(s) in a folder, and create an according *toc.yml* there - the rest is up to DocFx.

[!code-yaml[toc.yml](../restapi/toc.yml)]

In this example we use Swashbuckle for saving our API's structure. Install "Swashbuckle.AspNetCore" in your CalculatorWebApi project and add the following to your *.csproj file:

[!code-xml[csproj](../../../src/Calculator/CalculatorWebApi.csproj#L24-L26)]

and to configure swagger alter the project's *Startup.cs* file:

Add the following to your *ConfigureServices* method:

[!code-csharp[csproj](../../../src/Calculator/Startup.cs#L31-L34)]

And this line to *Configure*:

[!code-csharp[csproj](../../../src/Calculator/Startup.cs#L51-L51)]

Now everytime we build this project a swagger json file is generated.

## Adding conceptual markdown-files

Often it's useful to have some verbal explanation along the technical documentation. For that, markdown-files can be added. Just place them in a folder and add them to its toc-file. (In this example the conceptual files are located in /articles )

```md
# Conceptual files
can be useful often.
```

## Filtering unnecessary elements

Some information doesn't help us understand software better. Therefore certain files or code can be hidden from docs.
For that we just create a *filterConfig.yml* file in the docs root folder. DocFx looks which code is matched by the regular expressions defined in the file.

We've excluded the *Program* and *Startup* classes, as their job ist just setting up the projects and some external library code we don't need to know about.

> Note: If several exclusion rules for an element are defined, the uppermost will apply.

[!code-yaml[filterConfig.yml](../filterConfig.yml)]

Now add this line in your docfx.json config file within *metadata*:

[!code-json[metadata](../docfx.json#L13-L13)]

## Overwrite Files

Sometimes it can be useful to replace the automatically generated docs with a verbal text.
This can be done via *md*-files. Just specify the `uid` of the element you want to write your own text about.
You can either write text for a given category (e.g. `remarks`) or a general text that is placed at the top of the element's docs.

[!code-md[overwrite](../overwrite/Calculator.md)]

## Template Customization

DOCFX's templates can be overwritten and extended. Export the default template in `cmd` using:

```cmd
%UserProfile%\.nuget\packages\docfx.console\2.39.2\tools\docfx.exe template export default
```

now there's a new folder named *_exported_templates* with another folder inside called default.
Next, create a *templates* folder in your project's root. Copy the file *_exported_templates/default/styles/main.css* into this folder.
Be sure to also put it into a *styles* folder - if you want to override some of the default template's funcionality the file and directory structure has to be the same.
We just add some simple css for demonstration purposes:

[!code-css[styles](../templates/styles/main.css)]

Now we just have to include our *templates* folder in our *docfx.json* so that the changes can be applied:

[!code-json[metadata](../docfx.json#L47-L50)]

This will merge our own template with the default one.
After the next build our template should come into effect.

As a last step we edit a template partial for the REST documentation.
Unfortunately swagger type definitions are not supported by docfx's default template, that's why we modify the default template to hide the resulting empty rows.
For more information on that issue go to the [Github issue](https://github.com/dotnet/docfx/issues/2072)

Similiar to the above css replacement we navigate to the exported default folder and copy the file *partials/rest.child.tmpl.partial* to our template folder.

Remove the following code in the partial file:

```html
ln 34      <th>Type</th>
ln 35      <th>Value</th>
```

```html
ln 44      <td>{{type}}</td>
ln 45      <td>{{default}}</td>
```

```html
ln 61        <th>Samples</th>
```

```html
ln 70     <td class="sample-response">
            {{#examples}}
            <div class="mime-type">
              <i>Mime type: </i><span class="mime">{{mimeType}}</span>
            </div>
            <pre class="response-content"><code class="lang-js json hljs">{{content}}</code></pre>
            {{/examples}}
          </td>
```

The resulting folder structure of the templates-directory should look as follows:

```cmd
templates
  partials
    rest.child.tmpl.partial
  styles
    main.css
```

## The final docfx.json file

To round it up a short explanation of the docfx config file:

### metadata

[!code-json[metadata](../docfx.json#L2-L15)]

Docfx will generate metadata from .csproj files that are within the specified *src* folder, and put the results in the *api/* directory, doing all that with the specified filter configuration.
If you happen to have *cs* files that are in no projects, you can also include them in the files array.

### build

#### content

*Build* is responsible for putting it all together to make the docs ready to be served.

[!code-json[metadata](../docfx.json#L17-L29)]

in the content array the conceptual files to be included in the documentation are specified.

#### resource

[!code-json[metadata](../docfx.json#L30-L36)]

We don't use it in this example, but it's possible to include pictures in the documentation. The directory containing the images is specified here.

#### overwrite

[!code-json[metadata](../docfx.json#L37-L43)]

*overwrite* configures the path to markdownfiles replacing parts of the generated metadata docs.

#### template

[!code-json[docfx.json](../docfx.json#L47-L50)]

The top template (default in this case) is the first one to be applied, after that, our custom one (*templates* folder) overrides parts of the default one.

> Remember, for our template to override the default one, it has to resemble the original's folder structure.

## Deploying via Azure DevOps

In the solution root there is a file called *azure-pipelines.yml* where the configuration for building and deploying the application is.

We have 2 jobs, which are separated into different tasks and scripts. The first one (*BuildandTestCode*) runs on Linux, the other one (*BuildDocumentation*) on Windows, because *docfx.exe* is not cross-platform compatible (as it targets the full .NET-framework).

[!code-yaml[azure-pipelines](../../../azure-pipelines.yml)]