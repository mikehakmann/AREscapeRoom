from flask import Flask, request, jsonify

app = Flask(__name__)

# A variable to store the latest data received via POST
stored_data = {"value": 0}  # Initialize with a default value

@app.route('/', methods=['GET'])
def get_data():
    """Handle GET requests to fetch stored data."""
    return jsonify({"message": "Fetched data", "data": stored_data}), 200

@app.route('/', methods=['POST'])
def post_data():
    """Handle POST requests to receive and store data."""
    global stored_data
    data = request.get_json()
    if not data or "value" not in data:
        return jsonify({"error": "Invalid data"}), 400
    stored_data = data  # Update stored data
    return jsonify({"status": "Data received", "data": stored_data}), 201

if __name__ == "__main__":
    app.run(host="0.0.0.0", port=12345)  # Make the server accessible on the network
