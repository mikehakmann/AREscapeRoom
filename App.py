from flask import Flask, request, jsonify

app = Flask(__name__)

# A variable to store the latest data received via POST
stored_data = {}

@app.route('/', methods=['GET'])
def get_data():
    # Return the stored data if any has been received, or a default message
    if stored_data:
        return jsonify({"message": "Fetched data", "data": stored_data})
    else:
        return jsonify({"message": "No data has been received yet."})

@app.route('/', methods=['POST'])
def post_data():
    global stored_data
    # Get the JSON data from the request
    data = request.get_json()
    # Store it in the `stored_data` variable
    stored_data = data
    # Respond with a confirmation message
    return jsonify({"status": "Data received", "data": stored_data})

if __name__ == "__main__":
    app.run(port=5000)
