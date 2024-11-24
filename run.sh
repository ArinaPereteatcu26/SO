#!/bin/bash

# Script to build and run a C# project

# Navigate to the directory containing the .csproj file
PROJECT_DIR="."
cd "$PROJECT_DIR" || { echo "Project directory not found!"; exit 1; }

# Build the project
dotnet build --configuration Release
if [ $? -ne 0 ]; then
    echo "Build failed!"
    exit 1
fi

# Find the output directory and executable
OUTPUT_DIR=$(find "$PROJECT_DIR/output" -type d -name "*")
EXECUTABLE=$(find "$OUTPUT_DIR" -type f -name "*.dll")

if [ -z "$EXECUTABLE" ]; then
    echo "Executable not found!"
    exit 1
fi

# Run the executable
dotnet "$EXECUTABLE"
