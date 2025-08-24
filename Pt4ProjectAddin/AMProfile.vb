'Base class intended to be used to contain information respective of every AM technology and material combination.
'Acting as a template, designated as an abstract class with child classes for each combination.
Imports System.Collections.Generic

Public MustInherit Class AMProfile
	'properties include the materials and AM technology as strings
	Public Property Material As String
	Public Property Technology As String
	Protected Weights As Dictionary(Of String, Double)

	'common property with differing values for each AM technology
	'Mapping categorical inputs into numerical values of the criteria using dictionaries
	Protected MustOverride ReadOnly Property CategoryMappings As Dictionary(Of String, Dictionary(Of String, Integer))


	'other properties requiring main code to calculate first, before being passed to the class
	'fucntions are if-else statements checking the raw input and providing a reasonable value
	Protected MustOverride Function InterpretPartComplexity(rawComplexity As Double) As Double
	Protected MustOverride Function InterpretOverhangComplexity(rawOverhang As Double) As Double
	Protected MustOverride Function InterpretImpossibleFeatures(rawImpossibleFeatures As Boolean) As Double

	'construct default weightings for each AM profile (as each profile depending on part purpose may shift their respective weights)
	'each child class will initialise the weights in their constructor
	Public Sub New()
		Weights = New Dictionary(Of String, Double) From {
			{"Precision", 0.069}, {"LeadTime", 0.129}, {"PostProcessing", 0.152}, {"Volume", 0.15},
			{"Complexity", 0.241}, {"ImpossibleFeatures", 0.236}, {"Overhang", 0.024}
		}
	End Sub

    'Function to shift weightings according to the selected part purpose.
    Protected MustOverride Function IntendedPartPurpose(partPurpose As String) As Dictionary(Of String, Double)

    'A public sub to adjust the weights, as the protected function should only be called from the child class.
    Public Sub AdjustWeightsForPurpose(partPurpose As String)
		' Get the adjusted weights from the child class implementation
		Dim adjustedWeights = IntendedPartPurpose(partPurpose)
		' Overwrite the profile's internal Weights dictionary
		For Each key In adjustedWeights.Keys
			Weights(key) = adjustedWeights(key)
		Next
	End Sub


	'calculatescore function sums all values multiplied by their respective weights
	Public Function CalculateScore(criteria As ManufacturingCriteria) As Double
		Dim precisionValue As Double = CategoryMappings("Precision")(criteria.CategoricalInputs("Precision"))
		Dim leadTimeValue As Double = CategoryMappings("LeadTime")(criteria.CategoricalInputs("LeadTime"))
		Dim postProcessingValue As Double = CategoryMappings("PostProcessing")(criteria.CategoricalInputs("PostProcessing"))
		Dim volumeValue As Double = CategoryMappings("Volume")(criteria.CategoricalInputs("Volume"))

		' Interpret numeric inputs using the profile-specific functions
		Dim interpretedComplexity As Double = InterpretPartComplexity(criteria.NumericInputs("Complexity"))
		Dim interpretedOverhang As Double = InterpretOverhangComplexity(criteria.NumericInputs("Overhang"))
		Dim impossibleFeatureValue As Double = InterpretImpossibleFeatures(criteria.HasImpossibleFeatures)

		Dim score As Double = 0
		score += precisionValue * Weights("Precision")
		score += leadTimeValue * Weights("LeadTime")
		score += postProcessingValue * Weights("PostProcessing")
		score += volumeValue * Weights("Volume")
		score += interpretedComplexity * Weights("Complexity")
		score += interpretedOverhang * Weights("Overhang")
		score += impossibleFeatureValue * Weights("ImpossibleFeatures")
		Return score
	End Function

End Class


'helper class that ideally makes CalculateScore function easier
'doesn't require each class storing the user input, instead simply interpreting it as required for their respective AM technology
Public Class ManufacturingCriteria
	Public Property CategoricalInputs As Dictionary(Of String, String)
	Public Property NumericInputs As Dictionary(Of String, Double)
	Public Property HasImpossibleFeatures As Boolean

	Public Sub New(categorical As Dictionary(Of String, String),
				   numeric As Dictionary(Of String, Double),
				   impossibleFeatures As Boolean)
		Me.CategoricalInputs = categorical
		Me.NumericInputs = numeric
		Me.HasImpossibleFeatures = impossibleFeatures
	End Sub
End Class