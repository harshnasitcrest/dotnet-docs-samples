/*
 * Copyright 2025 Google LLC
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     https://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

// [START parametermanager_create_regional_param_version]

using Google.Cloud.ParameterManager.V1;
using Google.Protobuf;
using System.Text;


public class CreateRegionalParameterVersionSample
{
    /// <summary>
    /// This function creates a regional parameter version with an unformatted payload using the Parameter Manager SDK for GCP.
    /// </summary>
    /// <param name="projectId">The ID of the project where the parameter is located.</param>
    /// <param name="locationId">The ID of the region where the parameter is located.</param>
    /// <param name="parameterId">The ID of the parameter for which the version is to be created.</param>
    /// <param name="versionId">The ID of the version to be created.</param>
    /// <returns>The created ParameterVersion object.</returns>
    public ParameterVersion CreateRegionalParameterVersion(
        string projectId,
        string locationId,
        string parameterId,
        string versionId)
    {
        // Define the regional endpoint
        string regionalEndpoint = $"parametermanager.{locationId}.rep.googleapis.com";

        // Create the client with the regional endpoint
        ParameterManagerClient client = new ParameterManagerClientBuilder
        {
            Endpoint = regionalEndpoint
        }.Build();

        // Build the parent resource name using ParameterName
        ParameterName parent = new ParameterName(projectId, locationId, parameterId);

        // Convert the payload to bytes
        string payload = "test123";
        ByteString data = ByteString.CopyFrom(payload, Encoding.UTF8);

        // Build the parameter version with the unformatted payload
        ParameterVersion parameterVersion = new ParameterVersion
        {
            Payload = new ParameterVersionPayload
            {
                Data = data
            }
        };

        // Call the API to create the parameter version
        ParameterVersion createdParameterVersion = client.CreateParameterVersion(parent, parameterVersion, versionId);

        // Print the created parameter version name
        Console.WriteLine($"Created regional parameter version: {createdParameterVersion.Name}");

        // Return the created parameter version
        return createdParameterVersion;
    }
}
// [END parametermanager_create_regional_param_version]
