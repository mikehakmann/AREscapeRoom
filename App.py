from flask import Flask, request, jsonify
from waitress import serve

app = Flask(__name__)

# A variable to store the latest data received via POST
stored_data = {"switch": 0, "potentiometer": 0}  # Initialize with a default value

@app.route('/', methods=['GET'])
def get_data():
    """Handle GET requests to fetch stored data."""
    response = jsonify({"message": "Fetched data", "data": stored_data})
    return response  # No need to set 'Connection' header

@app.route('/', methods=['POST'])
def post_data():
    """Handle POST requests to receive and store data."""
    global stored_data
    try:
        data = request.get_json()
        print(data)
        if not data or "switch" not in data or "potentiometer" not in data:
            return jsonify({"error": "Invalid data"}), 400
        stored_data = data
        response = jsonify({"status": "Data received", "data": stored_data})
        return response, 201
    except Exception as e:
        print(f"Error processing POST request: {e}")  # Log error message
        return jsonify({"error": "Internal Server Error"}), 500

# Use Waitress to serve the app
if __name__ == "__main__":
    serve(app, host="0.0.0.0", port=12345)
