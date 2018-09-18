# Accessing Web Api using JavaScript

```js
var request = new XMLHttpRequest();
request.open('POST', 'https://localhost:[port]/api/Calculator', true);
request.setRequestHeader("Content-Type", "application/json");
request.onreadystatechange = function() {
  console.log(request.responseText);
}
request.send(JSON.stringify({
  "Dividend": 10,
  "Divisor": 5
}));
```