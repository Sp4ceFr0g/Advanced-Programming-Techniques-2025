<h1 align="center">📘 Advanced Programming Techniques</h1>
<p align="center"><i>My lab work for the course — a mix of experiments, broken stuff I fixed, and the “oh, that’s how it works” moments.</i></p>

<hr/>

<h2>✨ About this repo</h2>
<p>
Hey — this is my project folder for the <b>Advanced Programming Techniques</b> course.  
Inside are the labs I completed during the semester. I tried to keep each lab tidy and include notes on what I did and what I learned.  
This isn’t production code — it’s my learning log. If you read anything that looks messy, that’s probably where I learned the most.
</p>

<hr/>

<h2>📚 Contents (Labs 1 → 5)</h2>

<ol>
  <li><b>Lab 1 — Getting started & OOP basics</b></li>
  <li><b>Lab 2 — LINQ</b></li>
  <li><b>Lab 3 — Databases & EF Core</b></li>
  <li><b>Lab 4 — Async / Concurrency & Testing</b></li>
  <li><b>Lab 5 — Final integration / mini-project</b></li>
</ol>

---

<details>
<summary><b>Lab 1 — Getting started & OOP basics</b></summary>

<p>
This was the warm-up lab. I set up the project, learned the repo / build flow, and practiced fundamentals:
</p>

<ul>
  <li>Project & environment setup (solution layout, folders, compile/run)</li>
  <li>Basic OOP practice: classes, constructors, inheritance, and overriding <code>ToString()</code></li>
  <li>Small exercises to practice lists, loops, and simple I/O to the console</li>
  <li>Basic Git usage — init, add, commit, push (yep, I broke commits once and fixed it)</li>
</ul>

<p><b>What I learned:</b> how to set up a clean solution structure and why good naming matters. Also learned to commit often.</p>

</details>

---

<details>
<summary><b>Lab 2 — LINQ</b></summary>

<p>
This lab was a lot of fun — LINQ felt like writing SQL directly in C#. Tasks were done using both query and method syntax.
</p>

<ul>
  <li>Filter and transform number sets (e.g. select numbers &gt; 80, order descending)</li>
  <li>Transform numbers into strings like <code>"Have number #n"</code></li>
  <li>Work with people &amp; city datasets:
    <ul>
      <li>Filter by height</li>
      <li>Shorten names (<code>John Doe → J. Doe</code>)</li>
      <li>Collect distinct allergies</li>
      <li>Join people to cities and filter by population</li>
      <li>Convert a list of people to XML using <code>XElement</code></li>
    </ul>
  </li>
</ul>

<p><b>Snippet (example):</b></p>
<pre><code>// method syntax example
var bigNumbers = numbers.Where(n =&gt; n &gt; 80).OrderByDescending(n =&gt; n);
var transformed = bigNumbers.Select(n =&gt; $"Have number #{n}");
</code></pre>

<p><b>Files:</b> <code>Advanced ProgrammingTechniques 2Lab.pdf</code> (lab instructions & my solutions)</p>
</details>

---

<details>
<summary><b>Lab 3 — Databases & EF Core</b></summary>

<p>
Here I set up EF Core with SQLite and modelled entities and relationships. This was very practical — I finally understood how ORMs map objects to the database.
</p>

<ul>
  <li>Created <code>Student</code>, <code>Class</code>, and <code>Teacher</code> entity classes</li>
  <li>Configured a <code>DbContext</code> and worked with migrations</li>
  <li>Implemented relationships:
    <ul>
      <li>One-to-one (Class ↔ Teacher)</li>
      <li>One-to-many (Teacher ↔ Classes)</li>
      <li>Many-to-many (Students ↔ Classes)</li>
    </ul>
  </li>
  <li>Practice CRUD: add, query, remove, and re-query to see how FK constraints show up</li>
</ul>

<p><b>Quick commands I used:</b></p>
<pre><code>Install-Package Microsoft.EntityFrameworkCore.Sqlite
Install-Package Microsoft.EntityFrameworkCore.Tools
Add-Migration InitialCreate
Update-Database
</code></pre>

<p><b>Files:</b> <code>Advanced Programming Techniques Lab3.pdf</code> (lab instructions & my solutions)</p>
</details>

---

<details>
<summary><b>Lab 4 — Async / Concurrency & Testing</b></summary>

<p>
This lab pushed me into thinking about time: async operations, tasks, and simple thread-safety. I also started (very) basic unit tests.
</p>

<ul>
  <li>Implemented short async methods using <code>async/await</code> to simulate I/O-bound workflows</li>
  <li>Explored <code>Task</code> vs threads and learned pitfalls (race conditions, shared state)</li>
  <li>Wrote a couple of unit tests (xUnit) for key methods to practice TDD basics</li>
  <li>Added small synchronization where needed (locks / thread-safe collections)</li>
</ul>

<p><b>What I learned:</b> async makes code cleaner but debugging async flows requires careful logging. Tests make refactoring safer.</p>
</details>

---

<details>
<summary><b>Lab 5 — Final integration / mini-project</b></summary>

<p>
The final lab was about bringing things together. I created a small integrated demo that uses LINQ to filter data and EF Core to persist results — plus a tiny console interface to play with the data.
</p>

<ul>
  <li>Connected previous lab pieces: data models + queries + persistence</li>
  <li>Implemented a tiny “seed + query” console flow so you can run the program multiple times without duplicating data</li>
  <li>Polished <code>ToString()</code> on entities so output is readable when printed</li>
  <li>Added short README notes and run instructions (this file)</li>
</ul>

<p><b>What I learned:</b> how small, consistent patterns (seed, save, show) make demos reliable and easy to test.</p>
</details>

---

<h2>🛠️ How to run (general)</h2>
<p>
Most labs are console apps. Here’s a general recipe:
</p>
<pre><code>dotnet build
dotnet run --project ./path/to/your/project
</code></pre>

<p>
If you want to reset the SQLite DB used in Lab 3, delete the DB file (or clear the DbSet entries from code) and re-run migrations:
</p>
<pre><code>dotnet ef migrations add <NameIfChanging>
dotnet ef database update
// or delete the .db file and let EF recreate it if your code seeds data
</code></pre>

---

<h2>💬 Final notes (student voice)</h2>
<p>
This repo is me learning. There are bits that work, bits I broke, and bits I fixed at 2AM while wondering why the foreign key was null. If you’re browsing this, thanks for stopping by — and if anything here helps you, I’m glad I wrote it down.
</p>

<p align="center">— a student who learned a lot and still has TODOs in the code 😅</p>
