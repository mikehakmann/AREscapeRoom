from flask import Flask, request, jsonify
from waitress import serve

app = Flask(__name__)

# A variable to store the latest data received via POST
stored_data = {"switches": [], "potentiometer": 0.0}  # Initialize with default values

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
        # Validate the incoming data
        if not data or "switches" not in data or "potentiometer" not in data:
            return jsonify({"error": "Invalid data"}), 400
        
        # Ensure 'switches' is a list of integers
        if not isinstance(data["switches"], list) or not all(isinstance(s, int) for s in data["switches"]):
            return jsonify({"error": "'switches' must be a list of integers"}), 400
        
        # Ensure 'potentiometer' is a float or integer
        if not isinstance(data["potentiometer"], (float, int)):
            return jsonify({"error": "'potentiometer' must be a number"}), 400

        # Update the stored data
        stored_data = {
            "switches": data["switches"],
            "potentiometer": float(data["potentiometer"])  # Convert potentiometer to float
        }

        response = jsonify({"status": "Data received", "data": stored_data})
        return response, 201
    except Exception as e:
        print(f"Error processing POST request: {e}")  # Log error message
        return jsonify({"error": "Internal Server Error"}), 500

# Use Waitress to serve the app
if __name__ == "__main__":
    serve(app, host="0.0.0.0", port=12345)
