using System;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;

using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Collections.Generic;

namespace ConsoleApp3 {
    internal class DetectFacesController {



        public static List<BoundingBox> Example(string pic) {
            string accessKey = "AKIA5N5WQKZMRFLXPL7Y";
            string secretKey = "S29hBP8dUteCGR5on1np8KApfBvrHBpHvd42w4/u";
            string fileInfo = "";

            //AmazonS3Config config = new AmazonS3Config();
            //config.ServiceURL = "";

            String photo = pic;
            String bucket = "atlantisbucket";

            AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient(
            accessKey,
                    secretKey,
                    Amazon.RegionEndpoint.EUWest2
                    );
            DetectFacesRequest detectFacesRequest = new DetectFacesRequest() {
                Image = new Amazon.Rekognition.Model.Image() {
                    S3Object = new S3Object() {
                        Name = photo,
                        Bucket = bucket
                    }
                }
            };
            List<BoundingBox> boundingBoxes = new List<BoundingBox>();

            try {
                DetectFacesResponse detectFacesResponse = rekognitionClient.DetectFacesAsync(detectFacesRequest).GetAwaiter().GetResult();
                bool hasAll = detectFacesRequest.Attributes.Contains("ALL");


                foreach (FaceDetail face in detectFacesResponse.FaceDetails) {
                    //Console.WriteLine("BoundingBox: top={0} left={1} width={2} height={3}",
                    //    face.BoundingBox.Top,
                    //    face.BoundingBox.Left,
                    //    face.BoundingBox.Width,
                    //    face.BoundingBox.Height);
                    //Console.WriteLine("Confidence: {0}\nLandmarks: {1}\nPose: pitch={2} roll={3} yaw={4}\nQuality: {5}", face.Confidence, face.Landmarks.Count, face.Pose.Pitch, face.Pose.Roll, face.Pose.Yaw, face.Quality.Sharpness);


                    //    //Console.WriteLine(face.Quality.Sharpness);
                    //    foreach(var item in face.Emotions) {
                    //    Console.WriteLine(item);
                    //} 


                    //if (hasAll) {
                    //    Console.WriteLine("The detected face is estimated to be between " + face.AgeRange.Low + " and " + face.AgeRange.High + " years old.");
                    //}
                    boundingBoxes.Add(face.BoundingBox);
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            return boundingBoxes;

        }
    }
}
