package yeah.hack.filizanka.controller.dto;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;
import yeah.hack.filizanka.model.Carriage;

@Data
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class CarriageDto {

    private Long carriageId;

    private String carriageType;

    public static CarriageDto from(Carriage source) {
        return CarriageDto.builder()
                .carriageId(source.getCarriageId())
                .carriageType(source.getCarriageType())
                .build();
    }
}
