const express = require('express');
const getVersion = require('./index');

const app = express();
const port = 3000; // You can use any port you prefer

// Define a route for /api/version
app.get('/api/version', (req, res) => {
  const version = getVersion();
  res.json({ version });
});

// Start the server
app.listen(port, () => {
  console.log(`Server is running on http://localhost:${port}`);
});