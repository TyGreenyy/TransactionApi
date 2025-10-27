#!/bin/bash

# --- Configuration ---
aws_account_id="093795243081"
aws_region="us-east-1"
image_name="transaction-api"
# ---------------------

# Construct the full ECR image URI
ecr_uri="$aws_account_id.dkr.ecr.$aws_region.amazonaws.com/$image_name:latest"

echo "Step 1: Building the Docker image..."
docker build -t $image_name .

echo "Step 2: Tagging the image for ECR..."
docker tag $image_name:latest $ecr_uri

echo "Step 3: Authenticating with ECR..."
aws ecr get-login-password --region $aws_region | docker login --username AWS --password-stdin $aws_account_id.dkr.ecr.$aws_region.amazonaws.com

echo "Step 4: Pushing the image to ECR..."
docker push $ecr_uri

echo "Deployment push complete. App Runner will now automatically start the new deployment."